<%@ Page Language="C#" MasterPageFile="~/MasterAll/MasterPageAll.master" AutoEventWireup="true" MaintainScrollPositionOnPostback="true" CodeFile="PatientPrescriptionEntry.aspx.cs" Inherits="OPD_PatientPrescriptionEntry" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript" language="javascript">
        function getItemData() {
            var srvname = document.getElementById("ctl00_ContentPlaceHolder1_txtreg").value;
            
            $.ajax({
                type: "POST",
                url: "PatientPrescriptionEntry.aspx/SearchByPatientName",
                data: "{ prefixText: '" + srvname + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: "true",
                cache: "false",
                success: function (res) {
                    //alert(res);
                    var datas = res.d;
                    
                    var markup = "<table width='100%'>";
                    for (var x = 0; x < datas.length; x++) {
                        //alert(datas[x]);

                        var testname = datas[x].split('~');
                        markup = markup + "<tr><td width='40%' style='border:1px;border-right:1px solid;'><a href='#' onclick='setvalue(" + x + ")'>" + testname[0] + "</a></td><td width='60%' style='border:1px;'><input type='hidden' id='id" + x + "' value='" + testname[0] + "'><input type='hidden' id='name" + x + "' value='" + testname[1] + "'><input type='hidden' id='ledgerid" + x + "' value='" + testname[2] + "'><a href='#' onclick='setvalue(" + x + ")'>" + testname[1] + "</a></td></tr>";
                    }
                    markup = markup + "</table>";
                    $("#srvdiv").html(markup);
                    $("#srvdiv").show();
                }
            });
        }


        function setvalue(indx) {
            //alert(indx);
            var regno = $("#id" + indx).val();
            var name = $("#name" + indx).val();
            //var ledgerid = $("#ledgerid" + indx).val();
            document.getElementById("ctl00_ContentPlaceHolder1_txtreg").value = regno;
            document.getElementById("ctl00_ContentPlaceHolder1_hdnregno").value = regno;
            document.getElementById("ctl00_ContentPlaceHolder1_txtPatientName").value = name;
            //document.getElementById("ctl00_ContentPlaceHolder1_hdnLedgerId").value = ledgerid;
            $("#srvdiv").hide()
        }


        function autoCompleteEx_ItemSelected(sender, args) {
            debugger;
            var regname = args.get_value().split('~');// alert(regname[0]);
            document.getElementById("ctl00_ContentPlaceHolder1_txtDocId").value = regname[0];
            document.getElementById("ctl00_ContentPlaceHolder1_txtdocname").value = regname[1];

            $("#txtdocname").focus();
            //$("#DropDownList4").val(regname[1]);
        }
    </script>

    
    <%--For Busy Loader .............................--%>



    <asp:UpdateProgress ID="UpdateProgress1" runat="server">
        <ProgressTemplate>

            <div id="aaa" class="progressBackgroundFilter"></div>
            <div id="bbb" class="processMessage">
                <img alt="Loading" src="../images/pwait.gif" />

            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>


    <%--For Busy Loader End.............................--%>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div id="h1">
                <div class="pageheader">
                    <asp:Label ID="Label1" runat="server">Patient Prescription Entry</asp:Label>
                </div>
                <table width="290px" cellpadding="0" cellspacing="0">
                    <tr>
                        <td>
                            <asp:Button Text="Entry" BorderStyle="None" ID="Tab1" Width="145px" Height="40px" CssClass="Initial" runat="server" OnClick="Tab1_Click" />

                        </td>
                        <td>
                            <asp:Button Text="List" BorderStyle="None" ID="Tab2" CssClass="Initial" Width="145px" Height="40px" runat="server" OnClick="Tab2_Click" />

                        </td>

                    </tr>
                </table>
                <div class="formbox">
                    <asp:MultiView ID="MainView" runat="server">
                        <asp:View ID="View1" runat="server">
                            <div class="form-sec">
                                <div class="form-sec-row">
                                    <label><strong>Prescription Id :<span style="color: red;">*</span></strong></label>
                                    <asp:TextBox ID="txtPrepId" runat="server" CssClass="textbox-medium1" ReadOnly="false" ClientIDMode="Static"></asp:TextBox>

                                    <div class="clear"></div>
                                </div>
                                <div>
                                    <strong><asp:Label ID="lblError" runat="server" Font-Bold="True"></asp:Label>
                                </div>
                                <asp:HiddenField ID="HiddenField1" runat="server" />
                                <div class="form-sec-row">
                                    <label><strong>Registration No :<span style="color: red;">*</span></strong></label>
                                    <asp:TextBox ID="txtreg" runat="server" CssClass="textbox-medium1" onkeyup="getItemData()"></asp:TextBox>
                                    <asp:HiddenField ID="hdnregno" runat="server"/>
                                    <div id="srvdiv" style="width:30%;position:absolute;z-index:1;max-height:150px;overflow:auto;display:none;border-left:1px solid;border-right:1px solid;border-top:1px solid;border-bottom:1px solid;background-color:white;"></div>
                                    <div class="clear"></div>
                                </div>
                                <div class="form-sec-row">
                                    <label><strong>Patient Name :</strong></label>
                                    <asp:TextBox ID="txtPatientName" runat="server" CssClass="textbox-medium1" ReadOnly="true"></asp:TextBox>
                                    <div class="clear"></div>
                                </div>
                                <div class="form-sec-row">
                                    <label><strong>Prescription Date :</strong></label>
                                    <asp:TextBox ID="txtDate" runat="server"  CssClass="textbox-medium1" TextMode="Date"></asp:TextBox>
                                    <div class="clear"></div>
                                </div>
                                <div class="form-sec-row">
                                    <label><strong> Doctor Name:</strong></label>
                                    <div style="display: none;">
                                        <asp:TextBox ID="txtreferal" runat="server" CssClass="textbox-medium1"></asp:TextBox>
                                    </div>
                                    <asp:TextBox ID="txtDocId" runat="server" CssClass="textbox-medium1" style="display:none;"></asp:TextBox>
                                   <asp:TextBox ID="txtdocname" runat="server" CssClass="textbox-medium1" ></asp:TextBox>
                                    <cc1:AutoCompleteExtender ServiceMethod="Searchdoc" OnClientItemSelected="autoCompleteEx_ItemSelected" MinimumPrefixLength="1"
                                                CompletionInterval="100" EnableCaching="false" CompletionSetCount="10" TargetControlID="txtdocname" ID="AutoCompleteExtender2" runat="server" 
                                                   FirstRowSelected = "false" ></cc1:AutoCompleteExtender>

                                    <div class="clear"></div>
                                </div>
                                <div class="form-sec-row">
                                    <table>
                                        <tr>
                                            <td style="width: 100%;" align="center">
                                                <div style="padding: 10px; width: 110%; height: 221px; overflow: auto;">
                                                    <asp:GridView ID="gvPrepdtl" runat="server" ShowHeader="true" AutoGenerateColumns="False" CssClass="grid" PagerStyle-CssClass="pgr"
                                                                   OnRowDataBound="gvPrepdtl_RowDataBound" Width="100%" style="margin-top: 0px" ShowFooter="false" ShowHeaderWhenEmpty="true" >
                                                        <AlternatingRowStyle CssClass="odd1" />
                                                        <RowStyle  CssClass="odd1" />
                                                        <HeaderStyle CssClass="titlebar" />
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="MEDICINE GROUP" Visible="true">
                                                              <ItemTemplate>
                                                                    <asp:DropDownList ID="ddlMEDICINEGROUP" runat="server" CssClass="textbox-medium1" Height="29px" Width="155px" AutoPostBack="True" OnSelectedIndexChanged="ddlMEDICINEGROUP_SelectedIndexChanged">
                                                                    </asp:DropDownList>
                                                                    <asp:TextBox ID="txtmedicinegrp" runat="server" Width="90%" Text='<%# Eval("GroupID") %>' Visible="false"></asp:TextBox>
                                                               </ItemTemplate>
                                                                    <HeaderStyle CssClass="titlebar" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                              <ItemStyle CssClass="titlebar" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="MEDICINE NAME" Visible="true">
                                                              <ItemTemplate>
                                                                    <asp:DropDownList ID="ddlMEDICINENAME" runat="server" CssClass="textbox-medium1" Height="29px" Width="155px" AutoPostBack="True">
                                                                    </asp:DropDownList>
                                                                  <asp:TextBox ID="txtmedicine" runat="server" Width="90%" Text='<%# Eval("MedicineId") %>' Visible="false"></asp:TextBox>
                                                               </ItemTemplate>
                                                                    <HeaderStyle CssClass="titlebar" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                              <ItemStyle CssClass="titlebar" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="DAILY DOSE" Visible="true">
                                                              <ItemTemplate>
                                                                    <asp:DropDownList ID="ddlDAILYDOSE" runat="server" CssClass="textbox-medium1" Height="29px" Width="155px" AutoPostBack="True">
                                                                    </asp:DropDownList>
                                                                  <asp:TextBox ID="txtdose" runat="server" Width="90%" Text='<%# Eval("Dose") %>' Visible="false"></asp:TextBox>
                                                               </ItemTemplate>
                                                                    <HeaderStyle CssClass="titlebar" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                              <ItemStyle CssClass="titlebar" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="DURATION" Visible="true">
                                                              <ItemTemplate>
                                                                    <asp:DropDownList ID="ddlDURATION" runat="server" CssClass="textbox-medium1" Height="29px" Width="155px">
                                                                    </asp:DropDownList>
                                                                  <asp:TextBox ID="txtduration" runat="server" Width="90%" Text='<%# Eval("Duration") %>' Visible="false"></asp:TextBox>
                                                               </ItemTemplate>
                                                                    <HeaderStyle CssClass="titlebar" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                              <ItemStyle CssClass="titlebar" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </div>

                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" style="width: 50%">
                                                <asp:Button ID="btnAddRow" runat="server" CssClass="submit-button"  Height="28px" Text="Add Row" onclick="btnAddRow_Click" TabIndex="6"   />
                                            </td>

                                        </tr>
                                    </table>
                                    
                                </div>
                                <div class="form-sec-row">
                                    <label><strong>Remarks :<span style="color: red;">*</span></strong></label>
                                    <asp:TextBox ID="txtRemarks" runat="server" CssClass="textbox-medium1" TextMode="MultiLine" Height="55px" Width="593px"></asp:TextBox>
                                    <asp:HiddenField ID="HiddenField2" runat="server"/>
                                </div>
                                <div class="form-sec-row">
                                        <asp:Button ID="Button1" runat="server" CssClass="submit-button" Text="Save"  Height="28px" />
                                 </div>
                            </div>
                        </asp:View>
                    </asp:MultiView>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
