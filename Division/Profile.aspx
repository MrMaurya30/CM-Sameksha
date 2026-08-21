<%@ Page Title="" Language="C#" MasterPageFile="~/Division/MasterDiv.master" AutoEventWireup="true" CodeFile="Profile.aspx.cs" Inherits="Division_Profile" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="section">
        <div class="section-header">
            <div class="section-header-icon">
                <svg viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round">
                    <path stroke="none" d="M0 0h24v24H0z" fill="none" />
                    <path d="M3 21l18 0" />
                    <path d="M5 21v-14l8 -4v18" />
                    <path d="M19 21v-10l-6 -4" />
                    <path d="M9 9l0 .01" />
                    <path d="M9 12l0 .01" />
                    <path d="M9 15l0 .01" />
                    <path d="M9 18l0 .01" />
                </svg>nhi 
            </div>
            <div class="section-header-text">
                <h2>Profile</h2>
            </div>
        </div>
        <div class="section-fields">
            <div class="field">
                <label class="section-label" for="txtname">Office Name</label>
                <asp:TextBox ID="txtname" runat="server" AutoCompleteType="Disabled" CssClass="form-fields" placeholer="Enter Name..."></asp:TextBox>
            </div>
            <div class="field">
                <label class="section-label" for="txtmobile">Mobile No.</label>
                <asp:TextBox ID="txtmobile" runat="server" AutoCompleteType="Disabled" CssClass="form-fields" placeholer="Enter Mobile No..."></asp:TextBox>
            </div>
            <div class="field">
                <label class="section-label" for="txtemail">Email ID</label>
                <asp:TextBox ID="txtemail" runat="server" AutoCompleteType="Disabled" CssClass="form-fields" placeholer="Enter Email ID..."></asp:TextBox>
            </div>
        </div>

        <div class="section-footer">
            <asp:Button runat="server" ID="btnsubmit" CssClass="save-btn" Text="Save Changes" OnClick="btnsubmit_Click" OnClientClick="return confirmProjectSubmit(); " />
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
            title: "Update Profile Details?",
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

