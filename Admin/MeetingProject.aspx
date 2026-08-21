<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="true" CodeFile="MeetingProject.aspx.cs" Inherits="Admin_MeetingProject" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   <div class="section">
<div class="search-bar">
    <div class="search">
        <asp:TextBox ID="txtfilter_box" CssClass="form-control" runat="server" placeholder="Search..." oninput="Search_Gridview();" AutoCompleteType="Disabled"></asp:TextBox>
    </div>
    <div class="mom">
        <asp:HyperLink
            ID="hlView"
            runat="server"
            Text="View Minutes of meeting"
            NavigateUrl='<%# string.IsNullOrEmpty(Eval("MeetingmintUpload").ToString()) ? "#" : ResolveUrl(Eval("MeetingmintUpload").ToString()) %>'
            Target="_blank"
            Visible="false">
        </asp:HyperLink>
        <asp:Label ID="lblMessage" runat="server" Visible="false" style="margin-left:10px; font-size:1.1rem"></asp:Label>
    </div>
    <asp:Panel ID="pnlUpload" runat="server" CssClass="upload-mom">
        <a href="javascript:void(0);"
            class="button response-btn align-center"
            onclick="openUploadPopupt(); return false;">
            <svg width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="icon icon-tabler icons-tabler-outline icon-tabler-upload">
                <path stroke="none" d="M0 0h24v24H0z" fill="none" />
                <path d="M4 17v2a2 2 0 0 0 2 2h12a2 2 0 0 0 2 -2v-2" />
                <path d="M7 9l5 -5l5 5" />
                <path d="M12 4l0 12" />
            </svg>
            Upload Document
        </a>
    </asp:Panel>
</div>
   <asp:GridView
       ID="grdproject"
       runat="server" OnRowCommand="gvLetters_RowCommand"
       AutoGenerateColumns="False"
       CssClass="table mt-20"
       EmptyDataText="No records found">
       <Columns>
           <asp:TemplateField HeaderText="Sr.No">
               <ItemTemplate>
                   <%# Container.DataItemIndex + 1 %>
               </ItemTemplate>
           </asp:TemplateField>
           <asp:TemplateField HeaderText="Meeting Date">
               <ItemTemplate><%#Eval("MeetingDate") %></ItemTemplate>
           </asp:TemplateField>
           <asp:TemplateField HeaderText="Project Name">
               <ItemTemplate>
                   <%# Eval("ProjectName") %>
               </ItemTemplate>
           </asp:TemplateField>
           <asp:TemplateField HeaderText="Division">
                <ItemTemplate><%# Eval("Division") %></ItemTemplate>
            </asp:TemplateField>
           <asp:TemplateField HeaderText="Department Name">
               <ItemTemplate><%# Eval("deptIdreply1") %></ItemTemplate>
           </asp:TemplateField>
           <asp:TemplateField HeaderText="District">
               <ItemTemplate><%#Eval("District") %></ItemTemplate>
           </asp:TemplateField>
           <asp:TemplateField HeaderText="Action">
               <ItemTemplate>
                   <div class="action-buttons align-center">
                       <asp:Button
                           ID="btnView"
                           runat="server"
                           Text="View"
                           CssClass="table-button button"
                           CommandName="View"
                           CommandArgument='<%# Eval("ID") %>' />
                   </div>
               </ItemTemplate>
           </asp:TemplateField>
       </Columns>
   </asp:GridView>
       
    </div>

   <div class="modal fade" id="uploadModal" tabindex="-1">
    <div class="modal-dialog">
        <div class="modal-content">
            <div class="modal-header">
                <h5 class="modal-title">Upload Minutes Of Meeting</h5>
                <button type="button" class="button" data-bs-dismiss="modal">✖</button>
            </div>
            <div class="modal-body">
                <asp:FileUpload ID="FileUpload1" runat="server" />
            </div>
            <div class="modal-footer">
                <asp:Button ID="btnlnkUpload" runat="server" Text="Upload" CssClass="onUploadButton" OnClick="btnUpload_Click" />
            </div>
        </div>
    </div>
</div>
<script src="../Scripts/bootstrap.bundle.min.js"></script>
<script>
    function Search_Gridview() {
        var strKey = document.getElementById("<% =txtfilter_box.ClientID %>");
        var strData = strKey.value.toLowerCase().split(" ");
  <%--  var rdbvalue = $('#<%=rdoProceedingType.ClientID%> input:checked').val();
    //alert();
    if (rdbvalue == "S") {--%>
        var tblData = document.getElementById("<%=grdproject.ClientID%>");
   <%-- }
    else if (rdbvalue == "B") {
        var tblData = document.getElementById("<%=gridBindCandidate.ClientID%>");
    }--%>
        var rows = tblData.getElementsByTagName("tr");
        var rowData;
        for (var i = 1; i < rows.length; i++) {
            debugger
            rowData = rows[i].innerHTML;
            var styleDisplay = 'none';
            for (var j = 0; j < strData.length; j++) {
                if (rowData.toLowerCase().indexOf(strData[j]) >= 0)
                    styleDisplay = '';
                else {
                    styleDisplay = 'none';
                    break;
                }
            }
            tblData.rows[i].style.display = styleDisplay;
        }
    }
    function openUploadPopupt() {
        var modalEl = document.getElementById("uploadModal");
        var modal = new bootstrap.Modal(modalEl);
        modal.show();
    }
</script>
</asp:Content>

