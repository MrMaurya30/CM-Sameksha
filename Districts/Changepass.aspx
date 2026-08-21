<%@ Page Title="" Language="C#" MasterPageFile="~/Districts/MasterDist.master" AutoEventWireup="true" CodeFile="Changepass.aspx.cs" Inherits="Districts_Changepass" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="section">

        <div class="section-header">
            <div class="section-header-icon">
                <svg viewBox="0 0 24 24" fill="none" stroke="currentColor"
                    stroke-width="2" stroke-linecap="round" stroke-linejoin="round">
                    <path stroke="none" d="M0 0h24v24H0z" fill="none" />
                    <path d="M5 13a2 2 0 0 1 2 -2h10a2 2 0 0 1 2 2v6a2 2 0 0 1 -2 2h-10a2 2 0 0 1 -2 -2v-6z" />
                    <path d="M11 16a1 1 0 1 0 2 0a1 1 0 0 0 -2 0" />
                    <path d="M8 11v-4a4 4 0 1 1 8 0v4" />
                </svg>
            </div>

            <div class="section-header-text">
                <h2>Change Password</h2>
            </div>
        </div>

        <div class="section-fields">

            <div class="field">
                <label class="section-label" for="txtPassword">
                    Current Password
                </label>

                <asp:TextBox
                    ID="txtPassword"
                    runat="server"
                    TextMode="Password"
                    AutoCompleteType="Disabled"
                    CssClass="form-fields">
                </asp:TextBox>
            </div>

            <div class="field">
                <label class="section-label" for="txtconfirmpasss">
                    New Password
                </label>

                <asp:TextBox
                    ID="txtconfirmpasss"
                    runat="server"
                    TextMode="Password"
                    AutoCompleteType="Disabled"
                    CssClass="form-fields">
                </asp:TextBox>
            </div>

            <div class="field">
                <label class="section-label" for="txtnewpass">
                    Confirm New Password
                </label>

                <asp:TextBox
                    ID="txtnewpass"
                    runat="server"
                    TextMode="Password"
                    AutoCompleteType="Disabled"
                    CssClass="form-fields">
                </asp:TextBox>
            </div>

        </div>

        <div class="section-footer">

            <asp:Button
                runat="server"
                ID="btnsubmit"
                Text="Change Password"
                CssClass="save-btn"
                OnClick="btnsubmit_Click" OnClientClick="return confirmProjectSubmit();"  />

        </div>

    </div>
    <script>
    var isProjectConfirmed = false;

    function confirmProjectSubmit() {
    
        // If we've already confirmed (this is the re-triggered click), let it through
        if (isProjectConfirmed) {
            return true;
        }

        swal({
            title: "Update Password?",
            text: "Please confirm all details are correct before submitting. This action cannot be undone.",
            type: "info",
            showCancelButton: true,
            confirmButtonColor: "#ff9800",
            cancelButtonColor: "#1f2937",
            confirmButtonText: "Yes, Submit",
            cancelButtonText: "Cancel",
            closeOnConfirm: true,
            closeOnCancel: true
        },
            function (isConfirm) {
                if (isConfirm) {
                    isProjectConfirmed = true;
                    document.getElementById('<%=btnsubmit.ClientID%>').click();
                }
            });

        return false;
    }
    </script>

</asp:Content>

