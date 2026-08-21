<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="true" CodeFile="Project.aspx.cs" Inherits="Admin_Project" %>

<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="ckeditor" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="http://vaani.neechalkaran.com/Scripts/google_jsapi.js" type="text/javascript"></script>
    <script type="text/javascript" src="http://www.kirchensoft.ch/Demo1/CKEditor/ckeditor.js"></script>
    <script type="text/javascript" src="http://www.kirchensoft.ch/Demo1/CKEditor/ckfinder/ckfinder.js"></script>
    <script type="text/javascript" lang="javascript">
        google.load("elements", "1", {
            packages: "transliteration"
        });
        function onLoad() {
            var options = {
                sourceLanguage: google.elements.transliteration.LanguageCode.ENGLISH,
                destinationLanguage: [google.elements.transliteration.LanguageCode.HINDI],
                shortcutKey: 'ctrl+g',
                transliterationEnabled: true
            };
            var control = new google.elements.transliteration.TransliterationControl(options);
            //TEXT1 is the id of the control which you will use for the transliteration.
        }
        google.setOnLoadCallback(onLoad);
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_initializeRequest(InitializeRequest);
        prm.add_endRequest(EndRequest);
        function InitializeRequest(sender, args) {
        }
        // this is called to re-init the google after update panel updates.
        function EndRequest(sender, args) {
            onLoad();
        }
        formShowMsg(); StartCKEditor();
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="section active">
        <div class="section-header">
            <div class="section-header-icon">
                <svg viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round">
                    <path stroke="none" d="M0 0h24v24H0z" fill="none" />
                    <path d="M12 19h-6a2 2 0 0 1 -2 -2v-11a2 2 0 0 1 2 -2h4l3 3h7a2 2 0 0 1 2 2v3.5" />
                    <path d="M16 19h6" />
                    <path d="M19 16v6" />
                </svg>
            </div>
            <div class="section-header-text">
                <h2>Add Project</h2>

            </div>

            <asp:LinkButton runat="server" ID="btnback" Text="Back" class="back-btn" OnClick="btnback_Click"></asp:LinkButton>

        </div>
                <div class="field">
            <label class="form-labels">परियोजना का नाम / Project Name</label>
            <asp:TextBox ID="txtProjectName"
                runat="server"
                CssClass="form-fields"
                placeholder="Enter Project Name"
                 required="required"
                AutoCompleteType="Disabled" />
        </div>
        <div class="add-project-form">
            <div class="field">
                <label class="form-labels">बैठक दिनांक / Meeting Date</label>
                <asp:TextBox ID="txtmeetingdate"
                    runat="server"
                    TextMode="Date"
                    CssClass="form-fields"
                    required="required"
                    AutoCompleteType="Disabled" />
            </div>

            <div class="field">
                <label class="form-labels">पत्र दिनांक / Letter Date</label>
                <asp:TextBox ID="txtleterdate"
                    runat="server"
                    TextMode="Date"
                    CssClass="form-fields"
                    AutoCompleteType="Disabled" />
            </div>

            <div class="field">
                <label class="form-labels">पत्र संख्या / Letter No</label>
                <asp:TextBox ID="txtletterno"
                    runat="server"
                    CssClass="form-fields"
                    placeholder="Enter Letter No"
                    AutoCompleteType="Disabled" />
            </div>

            <div class="field">
                <label class="form-labels">अधिकारी / Officer</label>
                <asp:TextBox ID="txtofficer"
                    runat="server"
                    CssClass="form-fields"
                    placeholder="Enter Officer Name"
                    AutoCompleteType="Disabled" />
            </div>

            <div class="field">
                <label class="form-labels">स्थान / Location</label>
                <asp:TextBox ID="txtLocation"
                    runat="server"
                    CssClass="form-fields"
                    placeholder="Enter Location"
                    AutoCompleteType="Disabled" />
            </div>

            <div class="field">
                <label class="form-labels">स्वीकृत तिथि / Approved Date</label>
                <asp:TextBox ID="txtApprovedDate"
                    runat="server"
                    TextMode="Date"
                    CssClass="form-fields"
                    AutoCompleteType="Disabled" />
            </div>

            <div class="field">
                <label class="form-labels">परियोजना लागत / Project Cost</label>
                <asp:TextBox ID="txtProjectCost"
                    runat="server"
                    CssClass="form-fields"
                    placeholder="Enter Project Cost"
                    AutoCompleteType="Disabled" />
            </div>

            <div class="field">
                <label class="form-labels">संशोधित लागत / Revised Cost</label>
                <asp:TextBox ID="txtRevisedCost"
                    runat="server"
                    CssClass="form-fields"
                    placeholder="Enter Revised Cost"
                    AutoCompleteType="Disabled" />
            </div>

            <div class="field">
                <label class="form-labels">निर्धारित पूर्णता तिथि / Scheduled Completion</label>
                <asp:TextBox ID="txtScheduledCompletion"
                    runat="server"
                    TextMode="Date"
                    CssClass="form-fields"
                    AutoCompleteType="Disabled" />
            </div>

            <div class="field">
                <label class="form-labels">संभावित पूर्णता तिथि / Anticipated Completion</label>
                <asp:TextBox ID="txtAnticipatedCompletion"
                    runat="server"
                    TextMode="Date"
                    CssClass="form-fields"
                    AutoCompleteType="Disabled" />
            </div>

            <div class="field">
                <label class="form-labels">भौतिक प्रगति / Physical Progress</label>
                <asp:TextBox ID="txtPhysicalProgress"
                    runat="server"
                    CssClass="form-fields"
                    placeholder="Enter Physical Progress"
                    AutoCompleteType="Disabled" />
            </div>

            <div class="field">
                <label class="form-labels">वित्तीय प्रगति / Financial Progress</label>
                <asp:TextBox ID="txtFinancialProgress"
                    runat="server"
                    placeholder="Enter Financial Progress"
                    AutoCompleteType="Disabled" />
            </div>
                        <div class="field">
                <label class="form-labels">मण्डल / Division</label>

                <div class="checkbox-container">
                    <label>--- Select Division ---</label>

                    <asp:DropDownList
                        ID="ddlDivision"
                        runat="server"
                        CssClass="form-control"
                        AutoPostBack="true"
                        OnSelectedIndexChanged="ddlDivision_SelectedIndexChanged">
                    </asp:DropDownList>

                </div>
            </div>
               <div class="field">
                <label class="form-labels">जिला / District</label>

                <div class="checkbox-container">
                    <label>--- Select District ---</label>
                   
                    <asp:CheckBoxList
                        ID="chkDistricts"
                        runat="server"
                        Enabled="false"
                        CssClass="form-control" RepeatDirection="Vertical" RepeatLayout="Flow" AutoCompleteType="Disabled">
                    </asp:CheckBoxList>
                </div>

            </div>
            <div class="field">
                <label class="form-labels">विभाग / Department</label>

                <div class="checkbox-container">
                    <label>--- Select Department ---</label>

                    <asp:TextBox
                        ID="txtSearchDepartment"
                        runat="server"
                        placeholder="Search Department..."
                        CssClass="form-control"
                        onkeyup="filterDepartment()" />

                    <asp:CheckBoxList runat="server"
                        ID="chkDepartment"
                        CssClass="form-control"
                        RepeatDirection="Vertical" RepeatLayout="Flow" AutoCompleteType="Disabled">
                    </asp:CheckBoxList>
                </div>
            </div>


        <asp:Button
            ID="btnSaveMeta"
            runat="server"
            Text="Save Project"
            CssClass="submit button align-center"
            OnClick="btnsaveAll_Click" OnClientClick="return confirmProjectSubmit();" />
        <div class="field field-wide" >

            <label class="form-labels">
                परियोजना का विवरण / Project Details

            </label>

            <ckeditor:CKEditorControl
                ID="CKEditorControl1"
                BasePath="/ckeditor/"
                Height="600px"
                runat="server">
            </ckeditor:CKEditorControl>
        </div>
        <div style="display: none;">
            <asp:TextBox
                ID="txtprojectdetails"
                runat="server"
                TextMode="MultiLine">
            </asp:TextBox>
            <asp:TextBox
                ID="txtmeta"
                runat="server"
                TextMode="MultiLine">
            </asp:TextBox>
        </div>
        <div class="field field-wide">
            <asp:Button
                ID="Button1"
                runat="server"
                Text="Save Project"
                CssClass="submit button align-center"
                Style="margin-left: 0px; margin-top: 20px"
                OnClick="btnsaveAll_Click" OnClientClick="return confirmProjectSubmit();" />
        </div>
    </div>
    </div>
<script>
    function filterCheckBoxList(searchBoxId, checkBoxListId) {

        var filter = document
            .getElementById(searchBoxId)
            .value
            .toLowerCase()
            .trim();

        var chkList = document.getElementById(checkBoxListId);
        var labels = chkList.getElementsByTagName("label");

        for (var i = 0; i < labels.length; i++) {

            var text = labels[i].innerText.toLowerCase();

            var checkbox = labels[i].previousElementSibling;
            var br = labels[i].nextElementSibling;

            if (text.indexOf(filter) > -1) {
                checkbox.style.display = "";
                labels[i].style.display = "";
                if (br) br.style.display = "";
            }
            else {
                checkbox.style.display = "none";
                labels[i].style.display = "none";
                if (br) br.style.display = "none";
            }
        }
    }

    function filterDepartment() {
        filterCheckBoxList(
            '<%= txtSearchDepartment.ClientID %>',
            '<%= chkDepartment.ClientID %>'
        );
    }
    
</script>
<script>
    var isProjectConfirmed = false;

    function confirmProjectSubmit() {

        // Project Name validation
        var projectName = document.getElementById('<%= txtProjectName.ClientID %>');

        if (projectName.value.trim() === "") {
            alert("Please enter Project Name.");
            projectName.focus();
            return false;
        }

        // Meeting Date validation
        var meetingDate = document.getElementById('<%= txtmeetingdate.ClientID %>');

        if (meetingDate.value.trim() === "") {
            alert("Please enter Meeting Date.");
            meetingDate.focus();
            return false;
        }

        // Prevent confirmation popup from appearing twice
        if (isProjectConfirmed) {
            return true;
        }

        swal({
            title: "Submit this project?",
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
                document.getElementById('<%= Button1.ClientID %>').click();
                }
            });

        return false;
    }
</script>
</asp:Content>
