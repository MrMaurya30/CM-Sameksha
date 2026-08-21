<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="true" CodeFile="Department.aspx.cs" Inherits="Admin_Department" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="section">
    <div class="section-header">
    <div class="section-header-icon">
        <svg viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round">
            <path stroke="none" d="M0 0h24v24H0z" fill="none" />
            <path d="M3 21l18 0" />
            <path d="M9 8l1 0" />
            <path d="M9 12l1 0" />
            <path d="M9 16l1 0" />
            <path d="M14 8l1 0" />
            <path d="M14 12l1 0" />
            <path d="M14 16l1 0" />
            <path d="M5 21v-16a2 2 0 0 1 2 -2h10a2 2 0 0 1 2 2v16" />
            <path d="M14 21v-3a2 2 0 0 0 -2 -2h-0a2 2 0 0 0 -2 2v3" />
        </svg>
    </div>
    <div class="section-header-text">
        <h2>Add Department</h2>
    </div>
</div>
    <div class="add-department-form">
        <div class="field">
            <label class="form-labels">Enter Department Name<samp style="color:red">*</samp></label>
            <asp:TextBox 
                ID="txtDepartment" 
                runat="server" 
                CssClass="form-fields"  AutoCompleteType="Disabled"
                placeholder="Enter Department Name">
            </asp:TextBox>
        </div>
        <asp:Button 
            ID="btnSave" 
            runat="server" 
            Text="Add Department" 
            CssClass="submit button align-center"
            OnClick="btnSave_Click" OnClientClick="return validation();"/>
    </div>
    <div class="department-list section subsection">
        <p class="subheading"><u>All Departments:</u></p>
        <div class="field">
            <asp:TextBox 
                ID="searchDept" 
                runat="server" 
                CssClass="form-fields"  AutoCompleteType="Disabled"
                placeholder="Search Department Name">
            </asp:TextBox>
        </div>
        <asp:GridView runat="server" ID="griddeptname"  AutoGenerateColumns="False"
            CssClass="table mt-20"
            EmptyDataText="No records found">
            <Columns>
                <asp:TemplateField HeaderText="Sr.No">
                    <ItemTemplate>
                        <%# Container.DataItemIndex +1 %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Department Name">
                    <ItemTemplate>
                        <%#Eval("Name") %>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
</div>

    <script>
        document.getElementById('<%= searchDept.ClientID %>').addEventListener('keyup', function () {
        var filter = this.value.trim().toLowerCase();
        var table = document.getElementById('<%= griddeptname.ClientID %>');
        if (!table) return;

        var rows = table.getElementsByTagName('tr');

        for (var i = 1; i < rows.length; i++) {
            var nameCell = rows[i].getElementsByTagName('td')[1];
            if (!nameCell) continue;

            var nameText = nameCell.textContent.toLowerCase();
            rows[i].style.display = nameText.indexOf(filter) > -1 ? '' : 'none';
        }
        });
 
    </script>
    <script>
        var isProjectConfirmed = false;

        function validation() {

            var textbox = document.getElementById('<%= txtDepartment.ClientID %>');

            if (textbox.value.trim() == "") {
                alert("Please enter Department.");
                textbox.focus();
                return false;
            }
            if (isProjectConfirmed) {
                return true;
            }

            swal({
                title: "Add this Department ?",
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
                        document.getElementById('<%=btnSave.ClientID%>').click();
                }
            });

            return false;
        }
    </script>
</asp:Content>
