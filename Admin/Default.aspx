<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Admin_Default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
    .meeting-card {
        display: inline-flex;
        flex-direction: column;
        align-items: center;
        justify-content: center;
        gap: 6px;
        margin-right: 20px;
        margin-bottom: 20px; 
    }
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="section">
    <div class="section-header">
        <div class="section-header-icon">
            <svg viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round">
                <path stroke="none" d="M0 0h24v24H0z" fill="none" />
                <path d="M4 4m0 1a1 1 0 0 1 1 -1h6a1 1 0 0 1 1 1v6a1 1 0 0 1 -1 1h-6a1 1 0 0 1 -1 -1z" />
                <path d="M4 16m0 1a1 1 0 0 1 1 -1h6a1 1 0 0 1 1 1v2a1 1 0 0 1 -1 1h-6a1 1 0 0 1 -1 -1z" />
                <path d="M16 12m0 1a1 1 0 0 1 1 -1h2a1 1 0 0 1 1 1v6a1 1 0 0 1 -1 1h-2a1 1 0 0 1 -1 -1z" />
                <path d="M16 4m0 1a1 1 0 0 1 1 -1h2a1 1 0 0 1 1 1v2a1 1 0 0 1 -1 1h-2a1 1 0 0 1 -1 -1z" />
            </svg>
        </div>
        <div class="section-header-text">
            <h2>Dashboard</h2>
        </div>
    </div>
        <p class="subheading"><u>Meetings Held:</u></p>
<asp:Repeater ID="rptDates" runat="server">
    <ItemTemplate>

        <a href='MeetingProject.aspx?NN=<%# Eval("MeetingDate") %>'
            class="button p-14 mr-20 align-center meeting-card">

            <div class="meeting-date">
                <%# Convert.ToDateTime(Eval("MeetingDate")).ToString("MMMM, dd, yyyy") %>
            </div>

           
                <div style="font-size:12px; margin-top:5px;">
        Projects : <%# Eval("ProjectCount") %>
    </div>

        </a>

    </ItemTemplate>
</asp:Repeater>
</div>


</asp:Content>
