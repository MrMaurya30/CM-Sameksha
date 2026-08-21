<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="true" CodeFile="ViewUpdate.aspx.cs" Inherits="Admin_ViewUpdate" %>

<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="ckeditor" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="http://vaani.neechalkaran.com/Scripts/google_jsapi.js" type="text/javascript">
    </script>
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
                    <path d="M9 5h-2a2 2 0 0 0 -2 2v12a2 2 0 0 0 2 2h10a2 2 0 0 0 2 -2v-12a2 2 0 0 0 -2 -2h-2" />
                    <path d="M9 3m0 2a2 2 0 1 1 4 0a2 2 0 0 1 -4 0" />
                    <path d="M9 12l.01 0" />
                    <path d="M13 12l2 0" />
                    <path d="M9 16l.01 0" />
                    <path d="M13 16l2 0" />
                </svg>
            </div>
            <div class="section-header-text">
                <h2>View Projects</h2>
                <p class="section-header-subtitle">Edit Project Details</p>
            </div>
              <asp:LinkButton runat="server" ID="lnknback" Text="Back" class="back-btn" OnClick="btnback_Click">
            </asp:LinkButton>
        </div>
          <div class="project-info project-info-flag">

    <p>
        <strong>Project ID:</strong>
        <asp:Label ID="lblProjectID" runat="server"></asp:Label>
    </p>

    <p>
        <strong>Date:</strong>
        <asp:Label ID="lblProjectDate" runat="server"></asp:Label>
    </p>

</div>
                    <div class="field">
                <label class="form-labels">परियोजना का नाम / Project Name</label>

                <asp:TextBox
                    ID="txtProjectName"
                    runat="server"
                    CssClass="form-fields"
                    AutoCompleteType="Disabled" />
            </div>
        <div class="add-project-form">

            <div class="field">
                <label class="form-labels">बैठक दिनांक / Meeting Date</label>
                <asp:TextBox ID="txtMeetingDate"
                    runat="server"
                    CssClass="form-fields"
                    TextMode="Date"
                    AutoCompleteType="Disabled" />
            </div>

            <div class="field">
                <label class="form-labels">पत्र दिनांक / Letter Date</label>
                <asp:TextBox ID="txtLetterDate"
                    runat="server"
                    CssClass="form-fields"
                    TextMode="Date"
                    AutoCompleteType="Disabled" />
            </div>

            <div class="field">
                <label class="form-labels">पत्र संख्या / Letter No</label>
                <asp:TextBox ID="txtLetterNo"
                    runat="server"
                    CssClass="form-fields"
                    AutoCompleteType="Disabled" />
            </div>

            <div class="field">
                <label class="form-labels">अधिकारी / Officer</label>
                <asp:TextBox ID="txtOfficer"
                    runat="server"
                    CssClass="form-fields"
                    AutoCompleteType="Disabled" />
            </div>

            <div class="field">
                <label class="form-labels">स्थान / Location</label>
                <asp:TextBox ID="txtLocation"
                    runat="server"
                    CssClass="form-fields"
                    AutoCompleteType="Disabled" />
            </div>

            <div class="field">
                <label class="form-labels">स्वीकृत तिथि / Approved Date</label>
                <asp:TextBox ID="txtApprovedDate"
                    runat="server"
                    CssClass="form-fields"
                    TextMode="Date"
                    AutoCompleteType="Disabled" />
            </div>

            <div class="field">
                <label class="form-labels">परियोजना लागत / Project Cost</label>
                <asp:TextBox ID="txtProjectCost"
                    runat="server"
                    CssClass="form-fields"
                    AutoCompleteType="Disabled" />
            </div>

            <div class="field">
                <label class="form-labels">संशोधित लागत / Revised Cost</label>
                <asp:TextBox ID="txtRevisedCost"
                    runat="server"
                    CssClass="form-fields"
                    AutoCompleteType="Disabled" />
            </div>

            <div class="field">
                <label class="form-labels">निर्धारित पूर्णता तिथि / Scheduled Completion</label>
                <asp:TextBox ID="txtScheduledCompletion"
                    runat="server"
                    CssClass="form-fields"
                    TextMode="Date"
                    AutoCompleteType="Disabled" />
            </div>

            <div class="field">
                <label class="form-labels">संभावित पूर्णता तिथि / Anticipated Completion</label>
                <asp:TextBox ID="txtAnticipatedCompletion"
                    runat="server"
                    CssClass="form-fields"
                    TextMode="Date"
                    AutoCompleteType="Disabled" />
            </div>

            <div class="field">
                <label class="form-labels">भौतिक प्रगति / Physical Progress</label>
                <asp:TextBox ID="txtPhysicalProgress"
                    runat="server"
                    CssClass="form-fields"
                    AutoCompleteType="Disabled" />
            </div>

            <div class="field">
                <label class="form-labels">वित्तीय प्रगति / Financial Progress</label>
                <asp:TextBox ID="txtFinancialProgress"
                    runat="server"
                    CssClass="form-fields"
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
             CssClass="form-control"
             RepeatDirection="Vertical"
             RepeatLayout="Flow">
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

                    <asp:CheckBoxList
                        ID="chkDepartment"
                        runat="server"
                        CssClass="form-control"
                        RepeatDirection="Vertical"
                        RepeatLayout="Flow">
                    </asp:CheckBoxList>

                </div>
            </div>
            <asp:Button
                runat="server"
                ID="Button1"
                Text="Update Project"
                OnClick="TxtEdit_Click"
                CssClass="submit button align-center" OnClientClick="return confirmProjectSubmit();"/>

            <div class="field field-wide">

                <label class="form-labels">
                    Project Details
                </label>

                <ckeditor:CKEditorControl
                    ID="CKEditorControl1"
                    runat="server"
                    BasePath="/ckeditor/"
                    Height="600px">
                </ckeditor:CKEditorControl>

            </div>

            <div class="field field-wide">

                <asp:Button
                    ID="TxtEdit"
                    runat="server"
                    Text="Update Project"
                    CssClass="submit button align-center"
                    style="margin-left: 0px;margin-top: 20px"
                    OnClick="TxtEdit_Click" OnClientClick="return confirmProjectSubmit();"/>

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
    
        // If we've already confirmed (this is the re-triggered click), let it through
        if (isProjectConfirmed) {
            return true;
        }

        swal({
            title: "Update this project?",
            text: "Please confirm all details are correct before submitting.",
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
                    document.getElementById('<%=TxtEdit.ClientID%>').click();
                }
            });

        return false;
    }
</script>
</asp:Content>
