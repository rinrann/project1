<%@ Page Language="C#" MasterPageFile="~/MasterAll/MasterPageAll.master" AutoEventWireup="true" CodeFile="Rep_Investwisecollection.aspx.cs" Inherits="Pathology_Rep_Investwisecollection" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <script type="text/javascript">
        function autoCompleteEx_ItemSelected(sender, args) {
            debugger;
            var regname = args.get_value().split('~');
            document.getElementById("ctl00_ContentPlaceHolder1_txtSrvId").value = regname[0];
            document.getElementById("ctl00_ContentPlaceHolder1_txtServiceName").value = regname[1];

        }

        function printDiv(divID) {
            var divElements = document.getElementById(divID).innerHTML;
            var oldPage = document.body.innerHTML;
            document.body.innerHTML = divElements;
            window.print();
            document.body.innerHTML = oldPage;
        }

        function getItemData() {
            var srvname = document.getElementById("ctl00_ContentPlaceHolder1_txtServiceName").value;
            var srvgrp = document.getElementById("ctl00_ContentPlaceHolder1_ddlgroup").value;
            if (srvgrp != "") {
                $.ajax({
                    type: "POST",
                    url: "Rep_Investwisecollection.aspx/SearchServiceName",
                    data: "{ prefixText: '" + srvname + "',srvgrp: '" + srvgrp + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    async: "true",
                    cache: "false",
                    success: function (res) {
                        var datas = res.d;
                        //alert(datas);
                        var markup = "<table width='100%'>";
                        for (var x = 0; x < datas.length; x++) {
                            //alert(datas[x]);
                            var testname = datas[x].split('~');
                            markup = markup + "<tr><td width='100%'><a href='#' onclick='setvalue(" + testname[0] + ")'>" + testname[1] + "</a></td></tr>";
                        }
                        markup = markup + "</table>";
                        $("#srvdiv").html(markup);
                        $("#srvdiv").show();
                    }
                });
            }
        }

        function getServiceName() {
            var srvname = document.getElementById("ctl00_ContentPlaceHolder1_txtServiceName").value;
            var srvgrp = document.getElementById("ctl00_ContentPlaceHolder1_ddlgroup").value;
            if (srvgrp != "")
            {
                $.ajax({
                    type: "POST",
                    url: "Rep_Investwisecollection.aspx/SearchServiceName",
                    data: "{ prefixText: '" + srvname + "',srvgrp: '" + srvgrp + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    async: "true",
                    cache: "false",
                    success: function (res) {
                        var datas = res.d;
                        //alert(datas);
                        var markup = "<table width='100%'>";
                        for (var x = 0; x < datas.length; x++) {
                            //alert(datas[x]);
                            var testname = datas[x].split('~');
                            markup = markup + "<tr><td width='100%'><input type='hidden' id='id" + x + "' value='" + testname[0] + "'><input type='hidden' id='name" + x + "' value='" + testname[1] + "'><a href='#' onclick='setvalue(" + x + ")'>" + testname[1] + "</a></td></tr>";
                        }
                        markup = markup + "</table>";
                        $("#srvdiv").html(markup);
                        $("#srvdiv").toggle();
                    }
                });
            }
        }


        function setvalue(indx) {
            var testid = $("#id" + indx).val();
            var testname = $("#name" + indx).val();
            document.getElementById("ctl00_ContentPlaceHolder1_txtSrvId").value = testid;
            document.getElementById("ctl00_ContentPlaceHolder1_txtServiceName").value = testname;
            $("#srvdiv").hide()
        }
    </script>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div class="pageheader">
         <asp:Label ID="Label1" runat="server">INVESTIGATION WISE CULLECTION</asp:Label>
    </div>
    <div class="formbox">
        <div class="form-sec">
            <div class="error">
                <strong><asp:Label ID="lblError" runat="server" Font-Bold="True"></asp:Label></strong>
                <div class="clear"></div>
            </div>
        </div>
        <table style="width:100%">
            <tr>
                <td style="width:10%">
                    <label><strong>From Date :</strong></label> 
                </td>
                <td style="width:10%">
                    <asp:TextBox ID="txtfromdt" runat="server" TextMode="Date"></asp:TextBox>
                                   
                </td>
                <td style="width:10%">
                    <label><strong>To Date :</strong></label> 
                </td>
                <td  style="width:10%">
                        <asp:TextBox ID="txttodt" runat="server" TextMode="Date"></asp:TextBox>
                </td>
                <td style="width:10%">
                    <strong>Service Group : </strong>
                </td>
                <td  style="width:10%">
                    <asp:DropDownList ID="ddlgroup" runat="server" CssClass="textbox-medium1" Width="90%" >
                    </asp:DropDownList>

                </td>
                <td style="width:10%">
                    <strong>Service Name : </strong>
                </td>
                <td  style="width:15%">
                    <div style="display:none;">
                        <asp:TextBox ID="txtSrvId" CssClass="textbox-medium1"  runat="server"  style="width:100%;" ></asp:TextBox>
                    </div>
                    
                    <asp:TextBox ID="txtServiceName" CssClass="textbox-medium1"  runat="server"  style="width:85%;" onkeyup="getItemData()"></asp:TextBox><img src="../Images/downArrow.jpg" alt="" height="22" onclick="getServiceName()"/>
                    <div id="srvdiv" style="width:20%;position:absolute;z-index:1;max-height:150px;overflow:auto;display:none;border-left:1px solid;border-right:1px solid;border-top:1px solid;border-bottom:1px solid;background-color:white;"></div>
                   <%--<cc1:AutoCompleteExtender ServiceMethod="SearchServiceName"    OnClientItemSelected="autoCompleteEx_ItemSelected"    MinimumPrefixLength="1"
                        CompletionInterval="100" EnableCaching="false" CompletionSetCount="10" TargetControlID="txtServiceName"  ID="AutoCompleteExtender1" runat="server" 
                       FirstRowSelected = "false" UseContextKey="true">
                    </cc1:AutoCompleteExtender>--%>
                </td>
                <td style="width:10%;">  
                   <asp:Button ID="btnGenRpt" runat="server" Text="Generate Report" CssClass="submit-generate" OnClick="btnGenRpt_Click"/>

               </td>
            </tr>
        </table>
        <table width="100%">
        <tr>        
            <td align="center">  
               <h3 id="hd" runat="server" visible="false"> Investigation Wise Collection</h3>
                  <div id='mydiv' style="overflow:auto;width:100%">              
                <asp:Literal ID="ltrReport" runat="server"></asp:Literal><br />

                                   </div>                  
            </td>
        </tr>
        <tr>
            <td align="center">
                   <asp:Button ID="BtnBack" runat="server" style="width:70px; font-size:x-small" Text="Back" OnClick="BtnBack_Click"  />
                <%--<asp:Button ID="btn_excel" runat="server" style="width:100px; font-size:x-small" Text="Export to Excel" OnClick="btn_excel_Click" />--%>
                <%--<input type="button" id="cmdPrint" value="Print" style="width:70px; font-size:x-small" onclick="javascript: printDiv('mydiv')"/>--%>

            </td>
        </tr>
        </table>
    </div>
</asp:Content>