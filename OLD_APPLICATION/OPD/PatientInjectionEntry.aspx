<%@ Page Language="C#" MasterPageFile="~/MasterAll/MasterPageAll.master" AutoEventWireup="true" MaintainScrollPositionOnPostback="true" CodeFile="PatientInjectionEntry.aspx.cs" Inherits="OPD_PatientInjectionEntry" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript" language="javascript">
        function getItemData() {
            var srvname = document.getElementById("ctl00_ContentPlaceHolder1_txtreg").value;

            $.ajax({
                type: "POST",
                url: "PatientInjectionEntry.aspx/SearchByPatientName",
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
                        markup = markup + "<tr><td width='40%' style='border:1px;border-right:1px solid;'><a href='#' onclick='setvalue(" + x + ")'>" + testname[0] + "</a></td><td width='55%' style='border-right:1px solid;'><input type='hidden' id='id" + x + "' value='" + testname[0] + "'><input type='hidden' id='name" + x + "' value='" + testname[1] + "'><input type='hidden' id='ledgerid" + x + "' value='" + testname[2] + "'><a href='#' onclick='setvalue(" + x + ")'>" + testname[1] + "</a></td><td width='5%' style=''> <input type='hidden' id='age" + x + "' value='" + testname[3] + "'><a href='#' onclick='setvalue(" + x + ")'>" + testname[3] + "</a></td></tr>";
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
            var age = $("#age" + indx).val();
            //var ledgerid = $("#ledgerid" + indx).val();
            document.getElementById("ctl00_ContentPlaceHolder1_txtreg").value = regno;
            document.getElementById("ctl00_ContentPlaceHolder1_hdnregno").value = regno;
            document.getElementById("ctl00_ContentPlaceHolder1_txtPatientName").value = name;
            document.getElementById("ctl00_ContentPlaceHolder1_txtage").value = age;
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
                    <asp:Label ID="Label1" runat="server">Patient Injection Entry</asp:Label>
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

                                <div>
                                    <strong>
                                        <asp:Label ID="lblError" runat="server" Font-Bold="True"></asp:Label>
                                </div>
                                <div class="form-sec-row">
                                    <label><strong>Registration No :<span style="color: red;">*</span></strong></label>
                                    <asp:TextBox ID="txtreg" runat="server" CssClass="textbox-medium1" onkeyup="getItemData()"></asp:TextBox>
                                    <asp:HiddenField ID="hdnregno" runat="server" />
                                    <div id="srvdiv" style="width: 30%; position: absolute; z-index: 1; max-height: 150px; overflow: auto; display: none; border-left: 1px solid; border-right: 1px solid; border-top: 1px solid; border-bottom: 1px solid; background-color: white;"></div>
                                    <label><strong>Date :</strong></label>
                                    <asp:TextBox ID="txtDocDate" runat="server" CssClass="textbox-medium1" TextMode="Date"></asp:TextBox>
                                    <div class="clear"></div>
                                </div>
                                <div class="form-sec-row">
                                </div>
                                <div class="form-sec-row">
                                    <label><strong>Patient Name :</strong></label>
                                    <asp:TextBox ID="txtPatientName" runat="server" CssClass="textbox-medium1" ReadOnly="true"></asp:TextBox>
                                    <label><strong>Age :</strong> </label>
                                    <asp:TextBox ID="txtage" runat="server" CssClass="textbox-medium1" ReadOnly="true"></asp:TextBox>
                                    <div class="clear"></div>
                                </div>

                                <div class="form-sec-row">
                                    <label><strong>Doc No :</strong></label>
                                    <asp:TextBox ID="txtdocno" runat="server" CssClass="textbox-medium1" ReadOnly="true"></asp:TextBox>
                                    <div class="clear"></div>
                                </div>

                                <div class="form-sec-row">

                                    <table>
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="txtheader" runat="server" CssClass="textbox-medium1" TextMode="MultiLine" Height="100px" Width="110%"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 100%;" align="center">
                                                <div style="padding: 10px; width: 110%; overflow: auto;">
                                                    <asp:GridView ID="gvPrepdtl" runat="server" ShowHeader="true" AutoGenerateColumns="False" CssClass="grid" PagerStyle-CssClass="pgr"
                                                        OnRowDataBound="gvPrepdtl_RowDataBound" Width="100%" Style="margin-top: 0px" ShowFooter="false" ShowHeaderWhenEmpty="true">
                                                        <AlternatingRowStyle CssClass="odd1" />
                                                        <RowStyle CssClass="odd1" />
                                                        <HeaderStyle CssClass="titlebar" />
                                                        <Columns>

                                                            <asp:TemplateField HeaderText="Date" HeaderStyle-Width="15%">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtdates" runat="server" Width="90%" Text='<%# Eval("Dates") %>' TextMode="Date"></asp:TextBox>
                                                                </ItemTemplate>
                                                                <HeaderStyle CssClass="titlebar" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                <ItemStyle CssClass="titlebar" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Day" HeaderStyle-Width="15%">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtNoofDays" runat="server" Width="90%" Text='<%# Eval("NoofDays") %>'></asp:TextBox>
                                                                </ItemTemplate>
                                                                <HeaderStyle CssClass="titlebar" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                <ItemStyle CssClass="titlebar" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="INJECTION NAME" Visible="true" HeaderStyle-Width="30%">
                                                                <ItemTemplate>
                                                                    <asp:DropDownList ID="ddlMEDICINENAME" runat="server" CssClass="textbox-medium1" Height="29px" Width="155px" AutoPostBack="True">
                                                                    </asp:DropDownList>
                                                                    <asp:TextBox ID="txtmedicine" runat="server" Width="90%" Text='<%# Eval("MedicineId") %>' Visible="false"></asp:TextBox>
                                                                </ItemTemplate>
                                                                <HeaderStyle CssClass="titlebar" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                <ItemStyle CssClass="titlebar" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="SIG" HeaderStyle-Width="20%">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtsig" runat="server" Width="90%" Text='<%# Eval("SIG") %>'></asp:TextBox>
                                                                </ItemTemplate>
                                                                <HeaderStyle CssClass="titlebar" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                <ItemStyle CssClass="titlebar" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="ADV" HeaderStyle-Width="20%">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtadv" runat="server" Width="90%" Text='<%# Eval("ADV") %>'></asp:TextBox>
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
                                                <asp:Button ID="btnAddRow" runat="server" CssClass="submit-button" Height="28px" Text="Add Row" OnClick="btnAddRow_Click" TabIndex="6" />
                                            </td>

                                        </tr>
                                        <tr>
                                            <td>
                                                <div>
                                                    <asp:TextBox ID="txtfooter" runat="server" CssClass="textbox-medium1" TextMode="MultiLine" Height="100px" Width="110%"></asp:TextBox>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: center">
                                                <div>
                                                    <h3><u>ADVICE TO BE FOLLOWED 2 DAYS BEFORE BEGINING</u></h3>
                                                    <asp:TextBox ID="txtAdvtobeFollowed" runat="server" CssClass="textbox-medium1" TextMode="MultiLine" Height="100px" Width="110%"></asp:TextBox>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: center">
                                                <div>
                                                    <h3><u>TO BRING THESE ITEMS ON THE DAY OF ADMISSION</u></h3>
                                                    <table border="1" cellspacing="0" cellpadding="0" style="width: 110%">
                                                        <tr>
                                                            <td style='width: 5%; text-align: center'></td>
                                                            <td style='width: 5%; text-align: center'>Srl</td>
                                                            <td style='width: 30%; text-align: center'>Items</td>
                                                            <td style='width: 40%; text-align: center'>Remarks</td>
                                                            <td style='width: 20%; text-align: center'>Qty</td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:CheckBox ID="chk1" runat="server" /></td>
                                                            <td>1.</td>
                                                            <td style="text-align: left">
                                                                <asp:TextBox ID="txtitem1" runat="server" Text="Top 21 G"></asp:TextBox></td>
                                                            <td>
                                                                <asp:TextBox ID="txtrem1" Width="98%" runat="server"></asp:TextBox></td>
                                                            <td>
                                                                <asp:TextBox ID="txtqty1" Width="98%" runat="server" Style="text-align: right">1 set</asp:TextBox></td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:CheckBox ID="chk2" runat="server" /></td>
                                                            <td>2.</td>
                                                            <td style="text-align: left">
                                                                <asp:TextBox ID="txtitem2" runat="server" Text="I.V. Set"></asp:TextBox></td>
                                                            <td>
                                                                <asp:TextBox ID="txtrem2" Width="98%" runat="server"></asp:TextBox></td>
                                                            <td>
                                                                <asp:TextBox ID="txtqty2" Width="98%" runat="server" Style="text-align: right">1 set</asp:TextBox></td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:CheckBox ID="chk3" runat="server" /></td>
                                                            <td>3.</td>
                                                            <td style="text-align: left">
                                                                <asp:TextBox ID="txtitem3" runat="server" Text="Injection Atropine"></asp:TextBox></td>
                                                            <td>
                                                                <asp:TextBox ID="txtrem3" Width="98%" runat="server"></asp:TextBox></td>
                                                            <td>
                                                                <asp:TextBox ID="txtqty3" Width="98%" runat="server" Style="text-align: right"> 1 ampl</asp:TextBox></td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:CheckBox ID="chk4" runat="server" /></td>
                                                            <td>4.</td>
                                                            <td style="text-align: left">
                                                                <asp:TextBox ID="txtitem4" runat="server" Text="Injection Calmpose"></asp:TextBox></td>
                                                            <td>
                                                                <asp:TextBox ID="txtrem4" Width="98%" runat="server"></asp:TextBox></td>
                                                            <td>
                                                                <asp:TextBox ID="txtqty4" Width="98%" runat="server" Style="text-align: right">1 ampl</asp:TextBox></td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:CheckBox ID="chk5" runat="server" /></td>
                                                            <td>5.</td>
                                                            <td style="text-align: left">
                                                                <asp:TextBox ID="txtitem5" runat="server" Text="Injection Fortwin"></asp:TextBox></td>
                                                            <td>
                                                                <asp:TextBox ID="txtrem5" Width="98%" runat="server"></asp:TextBox></td>
                                                            <td>
                                                                <asp:TextBox ID="txtqty5" Width="98%" runat="server" Style="text-align: right">1 ampl</asp:TextBox></td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:CheckBox ID="chk6" runat="server" /></td>
                                                            <td>6.</td>
                                                            <td style="text-align: left">
                                                                <asp:TextBox ID="txtitem6" runat="server" Text="Injection Reglan"></asp:TextBox></td>
                                                            <td>
                                                                <asp:TextBox ID="txtrem6" Width="98%" runat="server"></asp:TextBox></td>
                                                            <td>
                                                                <asp:TextBox ID="txtqty6" Width="98%" runat="server" Style="text-align: right">1 ampl</asp:TextBox></td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:CheckBox ID="chk7" runat="server" /></td>
                                                            <td>7.</td>
                                                            <td style="text-align: left">
                                                                <asp:TextBox ID="txtitem7" runat="server" Text="Injection Tiniba (400)"></asp:TextBox></td>
                                                            <td>
                                                                <asp:TextBox ID="txtrem7" Width="98%" runat="server"></asp:TextBox></td>
                                                            <td>
                                                                <asp:TextBox ID="txtqty7" Width="98%" runat="server" Style="text-align: right">1 bottle</asp:TextBox></td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:CheckBox ID="chk8" runat="server" /></td>
                                                            <td>8.</td>
                                                            <td style="text-align: left">
                                                                <asp:TextBox ID="txtitem8" runat="server" Text="Injection Tet. tox"></asp:TextBox></td>
                                                            <td>
                                                                <asp:TextBox ID="txtrem8" Width="98%" runat="server"></asp:TextBox></td>
                                                            <td>
                                                                <asp:TextBox ID="txtqty8" Width="98%" runat="server" Style="text-align: right">1 ampl</asp:TextBox></td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:CheckBox ID="chk9" runat="server" /></td>
                                                            <td>9.</td>
                                                            <td style="text-align: left">
                                                                <asp:TextBox ID="txtitem9" runat="server" Text="Normal Salaine"></asp:TextBox></td>
                                                            <td>
                                                                <asp:TextBox ID="txtrem9" Width="98%" runat="server"></asp:TextBox></td>
                                                            <td>
                                                                <asp:TextBox ID="txtqty9" Width="98%" runat="server" Style="text-align: right">1 bottle</asp:TextBox></td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:CheckBox ID="chk10" runat="server" /></td>
                                                            <td>10.</td>
                                                            <td style="text-align: left">
                                                                <asp:TextBox ID="txtitem10" runat="server" Text="Injection Calvum"></asp:TextBox></td>
                                                            <td>
                                                                <asp:TextBox ID="txtrem10" Width="98%" runat="server"></asp:TextBox></td>
                                                            <td>
                                                                <asp:TextBox ID="txtqty10" Width="98%" runat="server" Style="text-align: right">1 vial</asp:TextBox></td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:CheckBox ID="chk11" runat="server" /></td>
                                                            <td>11.</td>
                                                            <td style="text-align: left">
                                                                <asp:TextBox ID="txtitem11" runat="server" Text="Injection Distilled water"></asp:TextBox></td>
                                                            <td>
                                                                <asp:TextBox ID="txtrem11" Width="98%" runat="server"></asp:TextBox></td>
                                                            <td>
                                                                <asp:TextBox ID="txtqty11" Width="98%" runat="server" Style="text-align: right">1 ampl</asp:TextBox></td>
                                                        </tr>
                                                        <tr>
                                                            <td></td>
                                                            <td>12.</td>
                                                            <td style="text-align: left">Disposable Syringe</td>
                                                            <td></td>
                                                            <td></td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:CheckBox ID="chk12" runat="server" /></td>
                                                            <td></td>
                                                            <td style="text-align: left">&nbsp;&nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtitem12" runat="server" Text=" 2ml."></asp:TextBox></td>
                                                            <td>
                                                                <asp:TextBox ID="txtrem12" Width="98%" runat="server"></asp:TextBox></td>
                                                            <td>
                                                                <asp:TextBox ID="txtqty12" Width="98%" runat="server" Style="text-align: right">4 syringes</asp:TextBox></td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:CheckBox ID="chk13" runat="server" /></td>
                                                            <td></td>
                                                            <td style="text-align: left">&nbsp;&nbsp;&nbsp;&nbsp;
                                                                <asp:TextBox ID="txtitem13" runat="server" Text=" 10ml."></asp:TextBox>.</td>
                                                            <td>
                                                                <asp:TextBox ID="txtrem13" Width="98%" runat="server"></asp:TextBox></td>
                                                            <td>
                                                                <asp:TextBox ID="txtqty13" Width="98%" runat="server" Style="text-align: right">2 syringes</asp:TextBox></td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </td>
                                        </tr>

                                        <tr>
                                            <td style="text-align: center">
                                                <asp:Button ID="btnsave" runat="server" CssClass="submit-button" Text="Save" Height="28px" OnClick="btnsave_Click" />
                                            </td>
                                        </tr>
                                    </table>

                                </div>
                            </div>
                        </asp:View>

                        <asp:View ID="View2" runat="server">
                            <div style='width: 100%;'>
                                <table width="100%" style='background-color: #FB7B13; color: #FFF;'>
                                    <tr style='border: 1px solid black;'>
                                        <td style='width: 110px;' align="center">Date</td>
                                        <td style='width: 90px;' align="center">Regn No</td>
                                        <td style='width: 70px;' align="center">Edit</td>
                                        <td style='width: 70px;' align="center" id="coldel" runat="server">Delete</td>
                                    </tr>
                                </table>
                            </div>
                            <div class="listing" style='width: 100%; height: 400px; overflow: auto;'>
                                <asp:GridView ID="GridView1" CssClass="grid" PagerStyle-CssClass="pgr" DataKeyNames="docno" runat="server"
                                    AutoGenerateColumns="False" AllowPaging="True" PageSize="100" OnRowCommand="GridView1_RowCommand" OnRowDataBound="GridView1_RowDataBound" Width="100%"
                                    OnRowDeleting="GridView1_RowDeleting" ShowHeader="false"
                                    OnPageIndexChanging="GridView1_PageIndexChanging" AllowSorting="True">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Date" ItemStyle-Width="90px" ItemStyle-HorizontalAlign="left">
                                            <ItemTemplate>
                                                &nbsp;<asp:Label ID="lblDocdate" runat="server" Text='<%# Bind("Docdate") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Registration No" ItemStyle-Width="90px" ItemStyle-HorizontalAlign="left">
                                            <ItemTemplate>
                                                &nbsp;<asp:Label ID="lblPatientRegNo" runat="server" Text='<%# Bind("PatientRegNo") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:CommandField SelectText="Edit" ShowSelectButton="True" ItemStyle-Width="70px" ItemStyle-HorizontalAlign="Center" ControlStyle-CssClass="temp" HeaderText="Edit" SelectImageUrl="~/Images/edit.png" ButtonType="Image">
                                            <ControlStyle CssClass="temp"></ControlStyle>
                                        </asp:CommandField>

                                        <asp:CommandField ShowDeleteButton="True" ControlStyle-CssClass="temp" ItemStyle-Width="70px" ItemStyle-HorizontalAlign="Center" HeaderText="Delete" DeleteImageUrl="~/Images/delete.png" ButtonType="Image">
                                            <ControlStyle CssClass="temp"></ControlStyle>
                                        </asp:CommandField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </asp:View>
                    </asp:MultiView>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
