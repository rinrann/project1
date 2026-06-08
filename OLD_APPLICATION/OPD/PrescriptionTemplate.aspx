<%@ Page Language="C#" MasterPageFile="~/MasterAll/MasterPageAll.master" AutoEventWireup="true"
    CodeFile="PrescriptionTemplate.aspx.cs" Inherits="Master_PrescriptionTemplate" %>
    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript" language="javascript">
    function ShowDialog() {

        var rtvalue = window.open("PrescriptionTempPopup.aspx", "sss", "Width:800px; Height:550px; dialogLeft:250px;");
        //document.getElementById("ctl00_ContentPlaceHolder1_HiddenField1").value = rtvalue.NameValue;
        //document.getElementById("ctl00_ContentPlaceHolder1_txtPrescrpTemName").value = rtvalue.ProfessionValue;
    }


    function SetContextKey() {
        $find('AutoCompleteExtender1').set_contextKey("GFC");
    }

    function autoCompleteEx_ItemSelected(sender, args) {

        var regname = args.get_value().split('~');// alert(regname[0]);
        //document.getElementById("ctl00_ContentPlaceHolder1_TextBoxicode").value = regname[1];
        document.getElementById("ctl00_ContentPlaceHolder1_txtPrescrpTemName").value = regname[0];

        $("#txtPrescrpTemName").focus();
        //$("#DropDownList4").val(regname[1]);
    }
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
    <div id="divContent" runat="server">
        <!-- Form Section html start -->
      
        <div class="pageheader">
            <asp:Label ID="Label1" runat="server">Prescription Template</asp:Label>
              
        </div>
        <div class="formbox">
            <div class="form-sec">
                <div class="error">
                    <strong>
                        <asp:Label ID="lblError" runat="server" Font-Bold="True" CssClass="fadeout"></asp:Label>
                    </strong>
                    <div class="clear">
                    </div>
                </div>
                <asp:HiddenField ID="HiddenField1" runat="server" Value="0" />
           <div class="form-sec-row">
                        <label>
                        <strong>
                       Template Group :</strong></label>
                        
        <asp:DropDownList ID="DropDownList51" runat="server" CssClass="textbox-medium1">
        </asp:DropDownList>
                                  <div class="clear">
                        </div>
                    </div>

                  <div class="form-sec-row">
                        <label>
                        <strong>
                        Template Name :</strong></label>
                          <asp:TextBox ID="txtPrescrpTemName" runat="server" CssClass="textbox-medium1" 
                              Enabled="true"></asp:TextBox>
                      <cc1:AutoCompleteExtender ServiceMethod="SearchTemplate" OnClientItemSelected="autoCompleteEx_ItemSelected" MinimumPrefixLength="1"
                                    CompletionInterval="100" EnableCaching="false" CompletionSetCount="10" TargetControlID="txtPrescrpTemName" ID="AutoCompleteExtender3" runat="server" 
                                       FirstRowSelected = "false" ></cc1:AutoCompleteExtender>
                               <asp:Button ID="Button3" runat="server" CssClass="submit-button" Text="SEARCH" Height="28px" OnClientClick="return ShowDialog();"/>
                    <asp:Button ID="Button4" runat="server" CssClass="submit-button" Text="FETCH"  Height="28px"  onclick="Button4_Click" />

                   <div class="clear">
                        </div>
                    </div>


                          <div class="form-sec-row">
                    <label><strong>
                      Checked By Medicine :</strong></label>
                    <asp:TextBox ID="txtCheckMedicine" runat="server" Width="700px" CssClass="textbox-medium1"></asp:TextBox>
    <cc1:AutoCompleteExtender ServiceMethod="SearchMedicine"   MinimumPrefixLength="2"    CompletionInterval="100" EnableCaching="false" 
                   CompletionSetCount="10" TargetControlID="txtCheckMedicine"  ID="AutoCompleteExtender1" runat="server" FirstRowSelected = "false" ></cc1:AutoCompleteExtender>
                    <div class="clear">
                    </div>
                </div>


                          <div class="form-sec-row">
                    <label><strong>
                      Checked By Sub Group :</strong></label>
                    <asp:TextBox ID="txtSubgroup" runat="server" Width="700px" CssClass="textbox-medium1"></asp:TextBox>
    <cc1:AutoCompleteExtender ServiceMethod="SearchSubGroup"   MinimumPrefixLength="1"    CompletionInterval="100" EnableCaching="false" 
                   CompletionSetCount="10" TargetControlID="txtSubgroup"  ID="AutoCompleteExtender2" runat="server" FirstRowSelected = "false" ></cc1:AutoCompleteExtender>
                    <div class="clear">
                    </div>
                </div>

                <div class="form-sec-row">
                    <div class="clear">
                    </div>
                </div>
                <table width="100%">
                 
                    <tr style='background-color:#FF9300;' >
                           <td align="center">
                  <div class="form-sec-row"> 
             <label class="lname"><strong>MEDICINE GROUP</strong></label> 
            </div>
        </td> 
                   
                      <td align="center">
                  <div class="form-sec-row"> 
             <label class="lname"><strong>MEDICINE SUB GROUP</strong></label> 
            </div>
        </td> 
                   
                   
                        <td align="center">
                  <div class="form-sec-row"> 
             <label class="lname"><strong>MEDICINE NAME</strong></label> 
            </div>
        </td> 
                        <td align="center">
                  <div class="form-sec-row"> 
             <label class="lname"><strong>ROUTE NAME</strong></label> 
            </div>
        </td> 
                        <td align="center">
                  <div class="form-sec-row"> 
             <label class="lname"><strong>DAILY DOSE</strong></label> 
            </div>
        </td> 
                        <td align="center">
                  <div class="form-sec-row"> 
             <label class="lname"><strong>DURATION</strong></label> 
            </div>
        </td> 
    
                    </tr>
                    <tr>
                        <td align="center">
                              <div class="form-sec-row">
                                  <asp:DropDownList ID="DropDownList1" runat="server" CssClass="textbox-medium1" 
                                      Height="29px" Width="155px" AutoPostBack="True" 
                                      onselectedindexchanged="DropDownList1_SelectedIndexChanged">
                                  </asp:DropDownList>
                           </div>
                        </td>

                         <td align="center">
                              <div class="form-sec-row">
                                  <asp:DropDownList ID="DropDownList41" runat="server" CssClass="textbox-medium1" 
                                      Height="29px" Width="155px" AutoPostBack="True" 
                                      onselectedindexchanged="DropDownList41_SelectedIndexChanged">
                                  </asp:DropDownList>
                           </div>
                        </td>

                     <td align="center">
                              <div class="form-sec-row">
                                  <asp:DropDownList ID="DropDownList2" runat="server" CssClass="textbox-medium1" 
                                      Height="29px" Width="155px"  >
                                  </asp:DropDownList>
                           </div>
                        </td>
                        <td align="center">
                              <div class="form-sec-row">
                                  <asp:DropDownList ID="DropDownList3" runat="server" CssClass="textbox-medium1" Height="29px" Width="155px">
                                  </asp:DropDownList>
                           </div>
                        </td>
                        <td align="center">
                             <asp:DropDownList ID="DropDownList31" runat="server" CssClass="textbox-medium1" Height="29px" Width="155px">
                                  </asp:DropDownList>
                        </td>
                        <td align="center">
                            <asp:DropDownList ID="ddlDuration1" runat="server" CssClass="textbox-medium1" Height="29px" Width="155px">
                    </asp:DropDownList>
                            </td>
                       <%--  <td align="center">
                            <asp:TextBox ID="TextBox2" runat="server" CssClass="textbox-medium1" Width="155px"></asp:TextBox>
                        </td>--%>
                    </tr>
                    <tr>
                        <td align="center">
                              <div class="form-sec-row">
                                  <asp:DropDownList ID="DropDownList4" runat="server" CssClass="textbox-medium1" 
                                      Height="29px" Width="155px" AutoPostBack="True" 
                                      onselectedindexchanged="DropDownList4_SelectedIndexChanged">
                                  </asp:DropDownList>
                           </div>
                        </td>
                     <td align="center">
                              <div class="form-sec-row">
                              

                                     <asp:DropDownList ID="DropDownList42" runat="server" CssClass="textbox-medium1" 
                                         Height="29px" Width="155px"  AutoPostBack="true"
                                         onselectedindexchanged="DropDownList42_SelectedIndexChanged">
                                  </asp:DropDownList>
                           </div>
                        </td>

                          <td align="center">
                              <div class="form-sec-row">
                                   <asp:DropDownList ID="DropDownList5" runat="server" CssClass="textbox-medium1" 
                                      Height="29px" Width="155px" 
                                       >
                                  </asp:DropDownList>
                           </div>
                        </td>
                        <td align="center">
                              <div class="form-sec-row">
                                  <asp:DropDownList ID="DropDownList6" runat="server" CssClass="textbox-medium1" Height="29px" Width="155px">
                                  </asp:DropDownList>
                           </div>
                        </td>
                         <td align="center">
                              <asp:DropDownList ID="DropDownList32" runat="server" CssClass="textbox-medium1" Height="29px" Width="155px">
                                  </asp:DropDownList>
                        </td>
                        <td align="center">
                            <asp:DropDownList ID="ddlDuration2" runat="server" CssClass="textbox-medium1" Height="29px" Width="155px">
                    </asp:DropDownList>
                            </td>
                        <%--  <td align="center">
                            <asp:TextBox ID="TextBox4" runat="server" CssClass="textbox-medium1" Width="155px"></asp:TextBox>
                        </td>--%>
                    </tr>
                    <tr>
                        <td align="center">
                              <div class="form-sec-row">
                                  <asp:DropDownList ID="DropDownList7" runat="server" CssClass="textbox-medium1" 
                                      Height="29px" Width="155px" AutoPostBack="True" 
                                      onselectedindexchanged="DropDownList7_SelectedIndexChanged">
                                  </asp:DropDownList>
                           </div>
                        </td>
                     <td align="center">
                              <div class="form-sec-row">
                                  <asp:DropDownList ID="DropDownList43" runat="server" CssClass="textbox-medium1" 
                                      Height="29px" Width="155px" AutoPostBack="True" 
                                      onselectedindexchanged="DropDownList43_SelectedIndexChanged">
                                  </asp:DropDownList>
                           </div>
                        </td>

                            <td align="center">
                              <div class="form-sec-row">
                                  <asp:DropDownList ID="DropDownList8" runat="server" CssClass="textbox-medium1" Height="29px" Width="155px">
                                  </asp:DropDownList>
                           </div>
                        </td>

                        <td align="center">
                              <div class="form-sec-row">
                                  <asp:DropDownList ID="DropDownList9" runat="server" CssClass="textbox-medium1" Height="29px" Width="155px">
                                  </asp:DropDownList>
                           </div>
                        </td>
                           <td align="center">
                                <asp:DropDownList ID="DropDownList33" runat="server" CssClass="textbox-medium1" Height="29px" Width="155px">
                                  </asp:DropDownList>
                        </td>
                        <td align="center">
                            <asp:DropDownList ID="ddlDuration3" runat="server" CssClass="textbox-medium1" Height="29px" Width="155px">
                    </asp:DropDownList>
                            </td>
                    <%--    <td align="center">
                            <asp:TextBox ID="TextBox6" runat="server" CssClass="textbox-medium1" Width="155px"></asp:TextBox>
                        </td>--%>
                    </tr>
                    <tr>
                        <td align="center">
                              <div class="form-sec-row">
                                  <asp:DropDownList ID="DropDownList10" runat="server" CssClass="textbox-medium1" 
                                      Height="29px" Width="155px" AutoPostBack="True" 
                                      onselectedindexchanged="DropDownList10_SelectedIndexChanged">
                                  </asp:DropDownList>
                           </div>
                        </td>
                     <td align="center">
                              <div class="form-sec-row">
                                  <asp:DropDownList ID="DropDownList44" runat="server" CssClass="textbox-medium1" 
                                      Height="29px" Width="155px" AutoPostBack="True" 
                                      onselectedindexchanged="DropDownList44_SelectedIndexChanged">
                                  </asp:DropDownList>
                           </div>
                        </td>

                               <td align="center">
                              <div class="form-sec-row">
                                  <asp:DropDownList ID="DropDownList11" runat="server" CssClass="textbox-medium1" Height="29px" Width="155px">
                                  </asp:DropDownList>
                           </div>
                        </td>

                        <td align="center">
                              <div class="form-sec-row">
                                  <asp:DropDownList ID="DropDownList12" runat="server" CssClass="textbox-medium1" Height="29px" Width="155px">
                                  </asp:DropDownList>
                           </div>
                        </td>
                       <td align="center">
                                <asp:DropDownList ID="DropDownList34" runat="server" CssClass="textbox-medium1" Height="29px" Width="155px">
                                  </asp:DropDownList>
                        </td>
                        <td align="center">
                            <asp:DropDownList ID="ddlDuration4" runat="server" CssClass="textbox-medium1" Height="29px" Width="155px">
                    </asp:DropDownList>
                            </td>
                   <%--      <td align="center">
                            <asp:TextBox ID="TextBox8" runat="server" CssClass="textbox-medium1" Width="155px"></asp:TextBox>
                        </td>--%>
                    </tr>
                    <tr>
                      <td align="center">
                              <div class="form-sec-row">
                                  <asp:DropDownList ID="DropDownList13" runat="server" CssClass="textbox-medium1" 
                                      Height="29px" Width="155px" AutoPostBack="True" 
                                      onselectedindexchanged="DropDownList13_SelectedIndexChanged">
                                  </asp:DropDownList>
                           </div>
                        </td>
                     <td align="center">
                              <div class="form-sec-row">
                                  <asp:DropDownList ID="DropDownList45" runat="server" CssClass="textbox-medium1" 
                                      Height="29px" Width="155px" AutoPostBack="True" 
                                      onselectedindexchanged="DropDownList45_SelectedIndexChanged">
                                  </asp:DropDownList>
                           </div>
                        </td>

                           <td align="center">
                              <div class="form-sec-row">
                                  <asp:DropDownList ID="DropDownList14" runat="server" CssClass="textbox-medium1" Height="29px" Width="155px">
                                  </asp:DropDownList>
                           </div>
                        </td>
                        <td align="center">
                              <div class="form-sec-row">
                                  <asp:DropDownList ID="DropDownList15" runat="server" CssClass="textbox-medium1" Height="29px" Width="155px">
                                  </asp:DropDownList>
                           </div>
                        </td>
                          <td align="center">
                                <asp:DropDownList ID="DropDownList35" runat="server" CssClass="textbox-medium1" Height="29px" Width="155px">
                                  </asp:DropDownList>
                        </td>
                        <td align="center">
                            <asp:DropDownList ID="ddlDuration5" runat="server" CssClass="textbox-medium1" Height="29px" Width="155px">
                    </asp:DropDownList>
                            </td>
            <%--    <td align="center">
                            <asp:TextBox ID="TextBox10" runat="server" CssClass="textbox-medium1" Width="155px"></asp:TextBox>
                        </td>--%>
                    </tr>
                    <tr>
                         <td align="center">
                              <div class="form-sec-row">
                                  <asp:DropDownList ID="DropDownList16" runat="server" CssClass="textbox-medium1" 
                                      Height="29px" Width="155px" AutoPostBack="True" 
                                      onselectedindexchanged="DropDownList16_SelectedIndexChanged">
                                  </asp:DropDownList>
                           </div>
                        </td>
                     <td align="center">
                              <div class="form-sec-row">
                                  <asp:DropDownList ID="DropDownList46" runat="server" CssClass="textbox-medium1" 
                                      Height="29px" Width="155px" AutoPostBack="True" 
                                      onselectedindexchanged="DropDownList46_SelectedIndexChanged">
                                  </asp:DropDownList>
                           </div>
                        </td>

                            <td align="center">
                              <div class="form-sec-row">
                                  <asp:DropDownList ID="DropDownList17" runat="server" CssClass="textbox-medium1" Height="29px" Width="155px">
                                  </asp:DropDownList>
                           </div>
                        </td>

                        <td align="center">
                              <div class="form-sec-row">
                                  <asp:DropDownList ID="DropDownList18" runat="server" CssClass="textbox-medium1" Height="29px" Width="155px">
                                  </asp:DropDownList>
                           </div>
                        </td>
                          <td align="center">
                                <asp:DropDownList ID="DropDownList36" runat="server" CssClass="textbox-medium1" Height="29px" Width="155px">
                                  </asp:DropDownList>
                        </td>
                        <td align="center">
                            <asp:DropDownList ID="ddlDuration6" runat="server" CssClass="textbox-medium1" Height="29px" Width="155px">
                    </asp:DropDownList>
                            </td>
                     <%--     <td align="center">
                            <asp:TextBox ID="TextBox12" runat="server" CssClass="textbox-medium1" Width="155px"></asp:TextBox>
                        </td>--%>
                    </tr>
                    <tr>
                   <td align="center">
                              <div class="form-sec-row">
                                  <asp:DropDownList ID="DropDownList19" runat="server" CssClass="textbox-medium1" 
                                      Height="29px" Width="155px" AutoPostBack="True" 
                                      onselectedindexchanged="DropDownList19_SelectedIndexChanged">
                                  </asp:DropDownList>
                           </div>
                        </td>
                     <td align="center">
                              <div class="form-sec-row">
                                  <asp:DropDownList ID="DropDownList47" runat="server" CssClass="textbox-medium1" 
                                      Height="29px" Width="155px" AutoPostBack="True" 
                                      onselectedindexchanged="DropDownList47_SelectedIndexChanged">
                                  </asp:DropDownList>
                           </div>
                        </td>

                                <td align="center">
                              <div class="form-sec-row">
                                  <asp:DropDownList ID="DropDownList20" runat="server" CssClass="textbox-medium1" Height="29px" Width="155px">
                                  </asp:DropDownList>
                           </div>
                        </td>

                        <td align="center">
                              <div class="form-sec-row">
                                  <asp:DropDownList ID="DropDownList21" runat="server" CssClass="textbox-medium1" Height="29px" Width="155px">
                                  </asp:DropDownList>
                           </div>
                        </td>
                      <td align="center">
                                <asp:DropDownList ID="DropDownList37" runat="server" CssClass="textbox-medium1" Height="29px" Width="155px">
                                  </asp:DropDownList>
                        </td>
                        <td align="center">
                            <asp:DropDownList ID="ddlDuration7" runat="server" CssClass="textbox-medium1" Height="29px" Width="155px">
                    </asp:DropDownList>
                            </td>
                     <%--    <td align="center">
                            <asp:TextBox ID="TextBox14" runat="server" CssClass="textbox-medium1" Width="155px"></asp:TextBox>
                        </td>--%>
                    </tr>
                    <tr>
                         <td align="center">
                              <div class="form-sec-row">
                                  <asp:DropDownList ID="DropDownList22" runat="server" CssClass="textbox-medium1" 
                                      Height="29px" Width="155px" AutoPostBack="True" 
                                      onselectedindexchanged="DropDownList22_SelectedIndexChanged">
                                  </asp:DropDownList>
                           </div>
                        </td>
                     <td align="center">
                              <div class="form-sec-row">
                                  <asp:DropDownList ID="DropDownList48" runat="server" CssClass="textbox-medium1" 
                                      Height="29px" Width="155px" AutoPostBack="True" 
                                      onselectedindexchanged="DropDownList48_SelectedIndexChanged">
                                  </asp:DropDownList>
                           </div>
                        </td>

                               <td align="center">
                              <div class="form-sec-row">
                                  <asp:DropDownList ID="DropDownList23" runat="server" CssClass="textbox-medium1" Height="29px" Width="155px">
                                  </asp:DropDownList>
                           </div>
                        </td>
                        <td align="center">
                              <div class="form-sec-row">
                                  <asp:DropDownList ID="DropDownList24" runat="server" CssClass="textbox-medium1" Height="29px" Width="155px">
                                  </asp:DropDownList>
                           </div>
                        </td>
                            <td align="center">
                                <asp:DropDownList ID="DropDownList38" runat="server" CssClass="textbox-medium1" Height="29px" Width="155px">
                                  </asp:DropDownList>
                        </td>
                        <td align="center">
                            <asp:DropDownList ID="ddlDuration8" runat="server" CssClass="textbox-medium1" Height="29px" Width="155px">
                    </asp:DropDownList>
                            </td>
                     <%--    <td align="center">
                            <asp:TextBox ID="TextBox16" runat="server" CssClass="textbox-medium1" Width="155px"></asp:TextBox>
                        </td>--%>
                    </tr>
                    <tr>
                       <td align="center">
                              <div class="form-sec-row">
                                  <asp:DropDownList ID="DropDownList25" runat="server" CssClass="textbox-medium1" 
                                      Height="29px" Width="155px" AutoPostBack="True" 
                                      onselectedindexchanged="DropDownList25_SelectedIndexChanged">
                                  </asp:DropDownList>
                           </div>
                        </td>
                     <td align="center">
                              <div class="form-sec-row">
                                  <asp:DropDownList ID="DropDownList49" runat="server" CssClass="textbox-medium1" 
                                      Height="29px" Width="155px" AutoPostBack="True" 
                                      onselectedindexchanged="DropDownList49_SelectedIndexChanged">
                                  </asp:DropDownList>
                           </div>
                        </td>

                           <td align="center">
                              <div class="form-sec-row">
                                  <asp:DropDownList ID="DropDownList26" runat="server" CssClass="textbox-medium1" Height="29px" Width="155px">
                                  </asp:DropDownList>
                           </div>
                        </td>
                        <td align="center">
                              <div class="form-sec-row">
                                  <asp:DropDownList ID="DropDownList27" runat="server" CssClass="textbox-medium1" Height="29px" Width="155px">
                                  </asp:DropDownList>
                           </div>
                        </td>
                          <td align="center">
                                <asp:DropDownList ID="DropDownList39" runat="server" CssClass="textbox-medium1" Height="29px" Width="155px">
                                  </asp:DropDownList>
                        </td>
                        <td align="center">
                            <asp:DropDownList ID="ddlDuration9" runat="server" CssClass="textbox-medium1" Height="29px" Width="155px">
                    </asp:DropDownList>
                            </td>
                     <%--    <td align="center">
                            <asp:TextBox ID="TextBox18" runat="server" CssClass="textbox-medium1" Width="155px"></asp:TextBox>
                        </td>--%>
                    </tr>
                    <tr>
                         <td align="center">
                              <div class="form-sec-row">
                                  <asp:DropDownList ID="DropDownList28" runat="server" CssClass="textbox-medium1" 
                                      Height="29px" Width="155px" AutoPostBack="True" 
                                      onselectedindexchanged="DropDownList28_SelectedIndexChanged">
                                  </asp:DropDownList>
                           </div>
                        </td>
                     <td align="center">
                              <div class="form-sec-row">
                                  <asp:DropDownList ID="DropDownList50" runat="server" CssClass="textbox-medium1" 
                                      Height="29px" Width="155px" AutoPostBack="True" 
                                      onselectedindexchanged="DropDownList50_SelectedIndexChanged">
                                  </asp:DropDownList>
                           </div>
                        </td>
                           <td align="center">
                              <div class="form-sec-row">
                                  <asp:DropDownList ID="DropDownList29" runat="server" CssClass="textbox-medium1" Height="29px" Width="155px">
                                  </asp:DropDownList>
                           </div>
                        </td>

                        <td align="center">
                              <div class="form-sec-row">
                                  <asp:DropDownList ID="DropDownList30" runat="server" CssClass="textbox-medium1" Height="29px" Width="155px">
                                  </asp:DropDownList>
                           </div>
                        </td>
                          <td align="center">
                                <asp:DropDownList ID="DropDownList40" runat="server" CssClass="textbox-medium1" Height="29px" Width="155px">
                                  </asp:DropDownList>
                        </td>
                        <td align="center">
                            <asp:DropDownList ID="ddlDuration10" runat="server" CssClass="textbox-medium1" Height="29px" Width="155px">
                    </asp:DropDownList>
                            </td>
                   <%--      <td align="center">
                            <asp:TextBox ID="TextBox20" runat="server" CssClass="textbox-medium1" Width="155px"></asp:TextBox>
                        </td>--%>
                    </tr>
                </table>
                <div class="form-sec-row">
                    &nbsp;&nbsp;&nbsp;<label><strong>&nbsp;</strong></label>
                    <asp:Button ID="Button1" runat="server" CssClass="submit-button" Height="28px" Text="Submit" OnClick="Button1_Click" />
                    <asp:Button ID="Button2" runat="server" CssClass="submit-button" Height="28px" Text="Cancel" OnClick="Button2_Click" />
                    <div class="clear">
                    </div>
                </div>
                <div class="clear">
                </div>
            </div>
        </div>
        
    <%-- </ContentTemplate>
    </asp:UpdatePanel>--%>
    </div>
    </ContentTemplate>
    </asp:UpdatePanel>
    
</asp:Content>
