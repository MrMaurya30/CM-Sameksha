<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="true" CodeFile="ProjectInfo.aspx.cs" Inherits="Admin_ProjectInfo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="section">
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
            </div>
        </div>
        <div class="search">
            <asp:TextBox ID="txtfilter_box" runat="server" placeholder="Search..." oninput="Search_Gridview();"></asp:TextBox>
        </div>
            <asp:GridView 
            ID="gvLetters" 
            runat="server" OnRowCommand="gvLetters_RowCommand" 
            AutoGenerateColumns="False"
            CssClass="table mt-20"
            EmptyDataText="No records found">
                <Columns>
                    <asp:TemplateField HeaderText="SrNo.">
                        <ItemTemplate><%# Container.DataItemIndex +1 %></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Meeting Date ">
                        <ItemTemplate><%#Eval("MeetingDate") %></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Letter Date">
                        <ItemTemplate>
                            <%# Eval("LetterDate", "{0:dd-MM-yyyy}") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Letter No">
                        <ItemTemplate><%# Eval("LetterNo") %></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Project Name">
                    <ItemTemplate><%# Eval("ProjectName") %></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Division">
                     <ItemTemplate> <%# Eval("Division") %> </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Districts">
                        <ItemTemplate><%# Eval("deptIdreply") %></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Department Name">
                     <ItemTemplate><%#Eval("deptIdreply1") %></ItemTemplate>
                        </asp:TemplateField>

                    <asp:TemplateField HeaderText="Action">
                        <ItemTemplate>
                            <div class="action-buttons align-center">
                            <asp:LinkButton
                                ID="btnView"
                                runat="server"
                                Text="View"
                                CssClass="table-button button"
                                CommandName="View"
                                CommandArgument='<%# Eval("ID") %>' />

                            <asp:LinkButton runat="server" Text="Edit"  CssClass="table-button button"
                                CommandArgument='<%# Eval("ID") %>' OnClick="btnedit_Click">                     
                            </asp:LinkButton>
                                </div>
                            </ItemTemplate>
                            </asp:TemplateField>
       

                </Columns>
            </asp:GridView>
    </div>


    <script>
    function Search_Gridview() {
        var strKey = document.getElementById("<% =txtfilter_box.ClientID %>");
        var strData = strKey.value.toLowerCase().split(" ");
      <%--  var rdbvalue = $('#<%=rdoProceedingType.ClientID%> input:checked').val();
        //alert();
        if (rdbvalue == "S") {--%>
        var tblData = document.getElementById("<%=gvLetters.ClientID%>");
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
    </script>

</asp:Content>