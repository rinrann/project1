<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LoginPage.aspx.cs" Inherits="LoginPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Welcome to eMedico</title>
    <link href="css/style.css" rel="Stylesheet" type="text/css" />
    <script type="text/javascript" src="//ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript">
        function DisableBackButton() {
            window.history.forward()
        }
        DisableBackButton();
        window.onload = DisableBackButton;
        window.onpageshow = function (evt) { if (evt.persisted) DisableBackButton() }
        window.onunload = function () { void (0) }

        $(document).ready(function () {
            $("input[id$='txtUserId']").val('User Name');

            $("input[id$='txtUserId']").blur(function () {
                if ($("input[id$='txtUserId']").val() == '') {
                    $("input[id$='txtUserId']").val('User Name');
                }
            });

            $("input[id$='txtUserId']").focus(function () {
                if ($("#txtUserId").val() == 'User Name') {
                    $("input[id$='txtUserId']").val('');
                }
            });

            $("#btnLogin").click(function () {
                if ($("input[id$='txtUserId']").val() == '') {
                    alert('Please Enter User Name Properly!');
                    $("input[id$='txtUserId']").focus();
                }
                else if ($("input[id$='txtPassword']").val() == '') {
                    alert('Please Enter Password Properly!');
                    $("input[id$='txtPassword']").focus();
                }
            });
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>

        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div id="pagebg">
                    <div id="page">
                        <div class="headernewimg">
                            <img src="images/banner.png" width="1360" height="477" />

                        </div>
                        <div class="wrapper-loginmain">
                            <div class="wrapper-login">


                                <div class="welcomesec">
                                    <img src="images/welcomenew.png" />
                                    <div class="clear"></div>
                                </div>
                                <div class="loginsec">
                                    <%--<h2>Login to eMedico</h2>--%>
                                    <b>
                                        <asp:Label ID="lblError" runat="server" Text="" ForeColor="Red">&nbsp;</asp:Label></b>
                                    <div class="logincon">
                                        <div class="loginheader">
                                            <img src="images/cms.png" style="height: 35px;" />
                                        </div>
                                        <div class="login-form-area">
                                            <asp:TextBox ID="txtUserId" runat="server" CssClass="textbox" Style="width: 240px;"></asp:TextBox>
                                            <asp:TextBox ID="txtPassword" runat="server" CssClass="textbox" TextMode="Password" Style="width: 240px;"></asp:TextBox>
                                            <asp:DropDownList ID="ddlCompany" runat="server" CssClass="textbox" Style="width: 245px;">
                                            </asp:DropDownList>
                                            <asp:Button ID="btnLogin" runat="server" Text="Login" OnClick="btnLogin_Click" CssClass="submitbutton" />


                                        </div>
                                    </div>
                                    <div class="clear"></div>
                                </div>
                                <div class="clear"></div>
                            </div>
                        </div>
                        <div class="clear"></div>
                    </div>
                    <div class="clear"></div>
                </div>
                <!-- Footer Section Start -->
                <div id="footer">
                    <div class="wrapper-footer">
                        <p align="center">
                            Ankuran - Care Compassion Cure : Kolkata -:- Helpline - 09876543210
                    <%--<br />
                    E-mail : gfchospital@gmail.com &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;    Website : www.gfchospital.com--%>
                        </p>
                    </div>
                    <div class="clear"></div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>

    </form>
</body>
</html>
