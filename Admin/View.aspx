<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="true" CodeFile="View.aspx.cs" Inherits="Admin_View" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:HiddenField ID="HidenID" runat="server" />
    <div id="view" class="section">
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
        <p class="section-header-subtitle">View Project Details</p>
    </div>
    <asp:LinkButton ID="btnBack"
        runat="server"
        Text="Back"
        CssClass="back-btn"
        OnClick="btnback_Click" />
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
<div class="section subsection">
    <div class="view-details">
        <p><strong>Meeting Date: </strong><asp:Label ID="lblMeetingDate" runat="server"></asp:Label></p>
        <p><strong>Letter Date: </strong><asp:Label ID="lblLetterDate" runat="server"></asp:Label></p>
        <p><strong>Letter No: </strong><asp:Label ID="lblLetterNo" runat="server"></asp:Label></p>
        <p><strong>Officer: </strong><asp:Label ID="lblOfficer" runat="server"></asp:Label></p>
        <p><strong>Location: </strong><asp:Label ID="lblLocation" runat="server"></asp:Label></p>
        <p><strong>Approved Date: </strong><asp:Label ID="lblApprovedDate" runat="server"></asp:Label></p>
        <p><strong>Project Cost: </strong><asp:Label ID="lblProjectCost" runat="server"></asp:Label></p>
        <p><strong>Revised Cost: </strong><asp:Label ID="lblRevisedCost" runat="server"></asp:Label></p>
        <p><strong>Scheduled Completion: </strong><asp:Label ID="lblScheduledCompletion" runat="server"></asp:Label></p>
        <p><strong>Anticipated Completion: </strong><asp:Label ID="lblAnticipatedCompletion" runat="server"></asp:Label></p>
        <p><strong>Physical Progress: </strong><asp:Label ID="lblPhysicalProgress" runat="server"></asp:Label></p>
        <p><strong>Financial Progress: </strong><asp:Label ID="lblFinancialProgress" runat="server"></asp:Label></p>
        <p><strong>Department: </strong><asp:Label ID="lblDepartment" runat="server"></asp:Label></p>
        <p><strong>Division: </strong><asp:Label ID="lblDivision" runat="server"></asp:Label></p>
        <p><strong>District: </strong><asp:Label ID="lblDistrict" runat="server"></asp:Label></p>
        <p><strong>Project Name: </strong><asp:Label ID="lblProjectName" runat="server"></asp:Label></p>
        <p></p>
          
    </div>
    <div class="project-details" style="margin-top:20px">
        <p><strong style="font-size: 1.2rem"><u>Project Details: </u></strong></p>
        <div class="details-text-wrapper">
            <asp:Label ID="lblProjectDetails" runat="server" CssClass="details-text"></asp:Label>
            <a id="btnReadMore" runat="server" href="javascript:void(0);" class="read-more-btn" onclick="toggleReadMore(this); return false;" visible="false">Read More</a>
        </div>
    </div>

</div>
        <div class="issues-section mt-20">
            <div class="create-issue" id="btnmsg" runat="server" visible="true">
                <asp:TextBox
                    runat="server"
                    ID="txtissue"
                    TextMode="MultiLine" AutoCompleteType="Disabled"
                    placeholder="Enter Issue Here">
                </asp:TextBox>
                <asp:Button
                    runat="server"
                    ID="btnIssue"
                    Text="Create Issue"
                    CssClass="button"
                    OnClick="btnIssue_Click" OnClientClick="return validateIssue(this);"
                    CommandArgument='<%# Eval("ID") %>' />
            </div>

            <asp:GridView ID="gvIssues" runat="server"
                AutoGenerateColumns="False" OnRowDataBound="gvIssues_RowDataBound"
                CssClass="table mt-20 issue-table" GridLines="None">
                <HeaderStyle CssClass="table-heading" />
                <Columns>

                    <asp:TemplateField HeaderText="Issues of Project" ItemStyle-CssClass="table-content">
                        <ItemTemplate>
                            <div class="issue-details">
                                <asp:Label ID="lblIssueText"
                                    CssClass="issue-text"
                                    runat="server"
                                    Text='<%# Eval("Issuetext") %>' />
                                <p class="response-info">
                                    By <%#Eval("Role") %> On <%# Eval("Iuusedate","{0:dd-MMM-yyyy hh:mm tt}") %>
                                </p>
                                <div class="response-header no-border">
                                    <asp:HyperLink
                                        runat="server"
                                        CssClass="button response-btn align-center"
                                        style="margin-right: 20px"
                                        NavigateUrl='<%# string.IsNullOrEmpty(Eval("img").ToString()) ? "#" : ResolveUrl(Eval("img").ToString()) %>'
                                        Target="_blank"
                                        Visible='<%# !string.IsNullOrEmpty(Eval("img").ToString()) %>'>
                                        <svg height="24" width="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="icon icon-tabler icons-tabler-outline icon-tabler-eye">
                                            <path stroke="none" d="M0 0h24v24H0z" fill="none" />
                                            <path d="M10 12a2 2 0 1 0 4 0a2 2 0 0 0 -4 0" />
                                            <path d="M21 12c-2.4 4 -5.4 6 -9 6c-3.6 0 -6.6 -2 -9 -6c2.4 -4 5.4 -6 9 -6c3.6 0 6.6 2 9 6" />
                                        </svg>
                                        View Image
                                    </asp:HyperLink>
                                    <!-- Upload FIRST -->
                                    <a href="javascript:void(0);"
                                        class="button response-btn align-center"
                                        onclick='<%# "openUploadPopup(" + Eval("ID") + "); return false;" %>'>
                                        <svg width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="icon icon-tabler icons-tabler-outline icon-tabler-upload">
                                            <path stroke="none" d="M0 0h24v24H0z" fill="none" />
                                            <path d="M4 17v2a2 2 0 0 0 2 2h12a2 2 0 0 0 2 -2v-2" />
                                            <path d="M7 9l5 -5l5 5" />
                                            <path d="M12 4l0 12" />
                                        </svg>
                                        Upload Image
                                    </a>
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Project Response" ItemStyle-CssClass="subtable">
                        <ItemTemplate>
                            <asp:Repeater ID="rptResponses"
                                runat="server">
                                <HeaderTemplate>
                                    <table>
                                        <tr>
                                            <th>Sr No</th>
                                            <th>Responses</th>
                                            <th>Actions</th>
                                        </tr>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:HiddenField runat="server" ID="btnhiden" Value='<%#Eval("ID") %>' />
                                    <itemtemplate>
                                        <tr>
                                            <td>
                                                <%# Eval("Hierarchy") %>
                                            </td>

                                            <td>
                                                <%# Eval("RespronsText") %>

                                                <div class="response-info">
                                                    By <%# Eval("deptIdreply") %>
                                                    On <%# Eval("replydate", "{0:dd-MMM-yyyy hh:mm tt}") %>

                                                    <a href="javascript:void(0);"
                                                        onclick="toggleReplyWithParent(this,
                                                        '<%# Eval("Role") %>',
                                                        '<%# Eval("deptIdreply") %>',
                                                        '<%# Eval("Hierarchy") %>',
                                                        '<%# Eval("replydate", "{0:dd-MMM-yyyy hh:mm tt}") %>',
                                                        '<%# Eval("ID") %>');"
                                                        class="button " style="margin-left: 5px">
                                                        Reply
                                                    </a>
                                                </div>
                                            </td>

                                            <td>
                                                <!-- View Image -->
                                                <asp:HyperLink
                                                    runat="server"
                                                    CssClass="button response-btn align-center"
                                                    style="margin-bottom: 10px"
                                                    NavigateUrl='<%# string.IsNullOrEmpty(Eval("img").ToString()) ? "#" : ResolveUrl(Eval("img").ToString()) %>'
                                                    Target="_blank"
                                                    Visible='<%# !string.IsNullOrEmpty(Eval("img").ToString()) %>'>
                                                    <svg height="24" width="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="icon icon-tabler icons-tabler-outline icon-tabler-eye">
                                                        <path stroke="none" d="M0 0h24v24H0z" fill="none" />
                                                        <path d="M10 12a2 2 0 1 0 4 0a2 2 0 0 0 -4 0" />
                                                        <path d="M21 12c-2.4 4 -5.4 6 -9 6c-3.6 0 -6.6 -2 -9 -6c2.4 -4 5.4 -6 9 -6c3.6 0 6.6 2 9 6" />
                                                    </svg>
                                                    View Image
                                                </asp:HyperLink>

                                                <!-- Upload -->
                                                <a href="javascript:void(0);"
                                                    class="button response-btn align-center"
                                                    onclick='<%# "openUploadPopupt(" + Eval("ID") + "); return false;" %>'>
                                                    <svg width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="icon icon-tabler icons-tabler-outline icon-tabler-upload">
                                                        <path stroke="none" d="M0 0h24v24H0z" fill="none" />
                                                        <path d="M4 17v2a2 2 0 0 0 2 2h12a2 2 0 0 0 2 -2v-2" />
                                                        <path d="M7 9l5 -5l5 5" />
                                                        <path d="M12 4l0 12" />
                                                    </svg>
                                                    Upload Image
                                                </a>
                                            </td>

                                        </tr>
                                    </itemtemplate>
                                </ItemTemplate>
                                <FooterTemplate>
                                    </table>
                                </FooterTemplate>
                            </asp:Repeater>
                            <!-- Reply Panel -->
                            <div class="response-header">
                                <asp:Panel ID="pnlReply" runat="server" Style="display: none;">
                                    <asp:HiddenField runat="server" ID="btnhiden" Value='<%#Eval("ID") %>' />

                                    <asp:HiddenField ID="hdnParentReplyId" runat="server" />
                                    <asp:Label ID="lblParentInfo"
                                        runat="server"
                                        Style="display: none; font-size: 15px; color: gray; margin-bottom: 5px; color: orange" />
                                    <div class="col-md-6">
                                        <asp:TextBox ID="txtresponse"
                                            runat="server"
                                            AutoCompleteType="Disabled" />
                                    </div>

                                    <div class="col-md-2">
                                        <asp:Button ID="btnresponse"
                                            runat="server"
                                            Text="Reply"
                                            CssClass="button response-btn align-center"
                                            style="margin: 5px 0"
                                            OnClick="btnresponse_Click" CommandArgument='<%#Eval("ID") %>' />
                                    </div>
                                </asp:Panel>
                                <a class="button response-btn align-center"
                                    href="javascript:void(0);"
                                    onclick="toggleSimpleReply(this);">
                                    <svg width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="icon icon-tabler icons-tabler-outline icon-tabler-message">
                                        <path stroke="none" d="M0 0h24v24H0z" fill="none" />
                                        <path d="M8 9h8" />
                                        <path d="M8 13h6" />
                                        <path d="M18 4a3 3 0 0 1 3 3v8a3 3 0 0 1 -3 3h-5l-5 3v-3h-2a3 3 0 0 1 -3 -3v-8a3 3 0 0 1 3 -3h12" />
                                    </svg>
                                    New Response
                                </a>
                            </div>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>

            <div class="modal fade" id="uploadModal" tabindex="-1">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h5 class="modal-title">Upload Image</h5>
                            <button type="button" class="button" data-bs-dismiss="modal">✖</button>
                        </div>
                        <div class="modal-body">
                            <asp:FileUpload ID="fileUpload2" runat="server" />
                        </div>
                        <div class="modal-footer">
                            <asp:Button ID="btnUpload" runat="server" Text="Upload" CssClass="onUploadButton" OnClick="btnUpload_Click" />
                        </div>
                    </div>
                </div>
            </div>
            <div class="modal fade" id="uploadModal1" tabindex="-1">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h5 class="modal-title">Upload Image</h5>
                            <button type="button" class="button" data-bs-dismiss="modal">✖</button>
                        </div>
                        <div class="modal-body">
                            <asp:FileUpload ID="fileUpload3" runat="server" />
                        </div>
                        <div class="modal-footer">
                            <asp:Button ID="tbnissuimg" runat="server" Text="Upload" CssClass="onUploadButton" OnClick="tbnissuimg_Click" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script type="text/javascript">
        function validateIssue(tbn) {
            var textbox = document.getElementById('<%= txtissue.ClientID %>');
            if (!textbox || textbox.value.trim() === "") {
                alert("Please enter issue.");
                return false;
            }
            return true;
        }

        function getIssueRow(el) {
            return el.closest("table").closest("tr");
        }

        function ensureOriginalHtml(btn) {
            if (btn._originalHtml === undefined) {
                btn._originalHtml = btn.innerHTML;
            }
        }

        function setBtnActive(btn) {
            ensureOriginalHtml(btn);
            btn.innerHTML = "✖";
        }

        function setBtnInactive(btn) {
            ensureOriginalHtml(btn);
            btn.innerHTML = btn._originalHtml;
        }

        function closeReplyPanel(panel) {
            if (panel.style.display !== "block") return;

            panel.style.display = "none";

            if (panel._activeBtn) {
                setBtnInactive(panel._activeBtn);
                panel._activeBtn = null;
            }

            if (panel._movedBtn) {
                var mb = panel._movedBtn;
                if (mb._originalParent) {
                    mb._originalParent.insertBefore(mb, mb._originalNext);
                }
                panel._movedBtn = null;
            }

            var hidden = panel.querySelector("[id*='hdnParentReplyId']");
            var label = panel.querySelector("[id*='lblParentInfo']");
            if (hidden) hidden.value = "";
            if (label) label.style.display = "none";

            if (panel._homeCell) {
                panel._homeCell.appendChild(panel);
            }
            if (panel._tempRow && panel._tempRow.parentNode) {
                panel._tempRow.parentNode.removeChild(panel._tempRow);
            }
            panel._anchorRow = null;
        }

        function openReplyPanelAt(panel, anchorRow, btn) {
            if (!panel._homeCell) {
                panel._homeCell = panel.parentNode;
            }
            if (!panel._tempRow) {
                panel._tempRow = document.createElement("tr");
                panel._tempRow.className = "reply-panel-row";
                var td = document.createElement("td");
                td.colSpan = 3;
                panel._tempRow.appendChild(td);
            }

            panel._tempRow.firstChild.appendChild(panel);
            anchorRow.parentNode.insertBefore(panel._tempRow, anchorRow.nextSibling);
            panel.style.display = "block";
            panel._anchorRow = anchorRow;
            panel._activeBtn = btn;
            setBtnActive(btn);
        }

        function toggleReplyWithParent(btn, role, deptIdreply, hierarchy, date, parentID) {
            var responseRow = btn.closest("tr");
            var gridRow = getIssueRow(responseRow);
            var panel = gridRow.querySelector("[id*='pnlReply']");
            var label = gridRow.querySelector("[id*='lblParentInfo']");
            var hidden = gridRow.querySelector("[id*='hdnParentReplyId']");
            if (!panel) return;

            if (panel.style.display === "block" && panel._anchorRow === responseRow) {
                closeReplyPanel(panel);
                return;
            }

            closeReplyPanel(panel);
            openReplyPanelAt(panel, responseRow, btn);

            if (label) {
                label.style.display = "block";
                label.innerHTML = "Replying To: " + deptIdreply + " | On: " + date;
            }
            if (hidden) hidden.value = parentID;
        }

        function toggleSimpleReply(btn) {
            var gridRow = btn.closest("tr");
            var panel = gridRow.querySelector("[id*='pnlReply']");
            var hidden = gridRow.querySelector("[id*='hdnParentReplyId']");
            var label = gridRow.querySelector("[id*='lblParentInfo']");

            if (!panel) return;

            if (panel.style.display === "block" && panel._anchorRow === gridRow) {
                closeReplyPanel(panel);
                return;
            }

            closeReplyPanel(panel);

            if (!btn._originalParent) {
                btn._originalParent = btn.parentNode;
                btn._originalNext = btn.nextSibling;
            }

            var replyColumn = panel.querySelector(".col-md-2");
            if (replyColumn) {
                replyColumn.appendChild(btn);
            }
            panel._movedBtn = btn;

            panel.style.display = "block";
            panel._anchorRow = gridRow;
            panel._activeBtn = btn;
            setBtnActive(btn);

            if (hidden)
                hidden.value = "";

            if (label)
                label.style.display = "none";
        }
        window.addEventListener("beforeunload", function () {
            sessionStorage.setItem("scrollPos", window.scrollY);
        });
        window.addEventListener("load", function () {
            var pos = sessionStorage.getItem("scrollPos");
            if (pos !== null) {
                window.scrollTo(0, parseInt(pos));
            }
        });
    </script>
    <!-- Bootstrap JS -->
    <script src="../Scripts/bootstrap.bundle.min.js"></script>
    <script>
        function openUploadPopupt(id) {

            document.getElementById('<%= HidenID.ClientID %>').value = id;

            var modalEl = document.getElementById("uploadModal");
            var modal = new bootstrap.Modal(modalEl);

            modal.show();
        }

        function openUploadPopup(id) {

            document.getElementById('<%= HidenID.ClientID %>').value = id;

            var modalEl = document.getElementById("uploadModal1");
            var modal = new bootstrap.Modal(modalEl);

            modal.show();
        }
        function toggleReadMore(link) {
            var wrapper = link.closest('.details-text-wrapper');
            var text = wrapper.querySelector('.details-text');

            if (text.classList.contains('collapsed')) {
                text.classList.remove('collapsed');
                text.style.maxHeight = text.scrollHeight + 'px';
                link.innerText = 'Read Less';
            } else {
                text.classList.add('collapsed');
                text.style.maxHeight = '';
                link.innerText = 'Read More';
            }
        }
        window.addEventListener("beforeunload", function () {
            sessionStorage.setItem("scrollPos", window.scrollY);
        });
        window.addEventListener("load", function () {
            var pos = sessionStorage.getItem("scrollPos");
            if (pos !== null) {
                window.scrollTo(0, parseInt(pos));
            }
        });
    </script>


</asp:Content>





