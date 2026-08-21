<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="DivisionLogin.aspx.cs" Inherits="DivisionLogin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>CM Sameksha</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <main>
        <div class="form">
            <p class="login-title" id="login-title">Login</p>
            <p class="login-desc">Please enter your credentials to continue</p>
            <div class="login-box">
                <div class="">
                    <asp:DropDownList runat="server" ID="ddlDivision" CssClass="select" AutoCompleteType="Disabled"></asp:DropDownList>
                    <div class="password-container">
                        <asp:TextBox ID="txtPassword" runat="server"
                            TextMode="Password"
                            CssClass="password" AutoCompleteType="Disabled"
                            placeholder="Password" />
                        <button type="button" class="toggle-password" id="toggle-password" aria-label="Show password">
                            <svg width="24" height="24" viewBox="0 0 576 512">
                              <path
                                d="M288 32c-80.8 0-145.5 36.8-192.6 80.6-46.8 43.5-78.1 95.4-93 131.1-3.3 7.9-3.3 16.7 0 24.6 14.9 35.7 46.2 87.7 93 131.1 47.1 43.7 111.8 80.6 192.6 80.6s145.5-36.8 192.6-80.6c46.8-43.5 78.1-95.4 93-131.1 3.3-7.9 3.3-16.7 0-24.6-14.9-35.7-46.2-87.7-93-131.1-47.1-43.7-111.8-80.6-192.6-80.6zM144 256a144 144 0 1 1 288 0 144 144 0 1 1 -288 0zm144-64c0 35.3-28.7 64-64 64-11.5 0-22.3-3-31.7-8.4-1 10.9-.1 22.1 2.9 33.2 13.7 51.2 66.4 81.6 117.6 67.9s81.6-66.4 67.9-117.6c-12.2-45.7-55.5-74.8-101.1-70.8 5.3 9.3 8.4 20.1 8.4 31.7z"
                              />
                            </svg>
                        </button>
                    </div>
                    <div class="captcha-container">
                        <div class="">
                            <asp:Label ID="lblCaptcha" runat="server"
                                AutoCompleteType="Disabled" CssClass="captcha-box" />
                        </div>
                        <div class="">

                            <asp:Button ID="btnRefresh" runat="server"
                                Text="↻"
                                CssClass="refresh-captcha"
                                OnClick="btnRefresh_Click" />
                        </div>
                    </div>
                    <div class="">
                        <asp:TextBox ID="txtCaptcha" runat="server"
                            CssClass="enter-captcha"
                            placeholder="Enter Captcha" AutoCompleteType="Disabled" />
                    </div>
                    <div class="">
                        <asp:Button ID="btnLogin" runat="server"
                            CssClass="submit"
                            Text="Login"
                            OnClick="btnLogin_Click" OnClientClick="return SHA512auth();" />
                    </div>
                </div>
            </div>
        </div>
    </main>
     <script src="Admin/js/SHA512.js"></script>
    <script language="javascript" type="text/javascript">
        function SHA512auth() {
            try {
                var result = 0;
               // var randomnumber = '<%=Session["Rand"]%>';
            var UName = document.getElementById("<%=ddlDivision.ClientID%>").value;
            if (UName == "") {
                alert("Please Enter UserID");
                result = 1;
                return false;
            }
            // alert(UName);
            var ency_password = document.getElementById("<%=txtPassword.ClientID %>").value;

            if (ency_password == "") {
                alert("Please Enter Password");
                result = 1;
                return false;
            }
            var hash1 = SHA512(ency_password);
            //  alert(hash1);
            //  var hash2 = SHA512(randomnumber + hash1);
            document.getElementById("<%=txtPassword.ClientID %>").value = hash1;
                return true;

            }
            catch (err) {
                txt = "There was an error on this page.\n\n";
                txt += "Error description: " + err.message + "\n\n";
                txt += "Click OK to continue.\n\n";
                alert(txt);
                return false;
            }
        }
    </script>
</asp:Content>

