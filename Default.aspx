<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link rel="stylesheet" href="Admin/css/home.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <main>
  <section class="selections">
    <a href="../Login.aspx?type=Administrator" class="card">
      <!-- <i class="fa-solid fa-user-shield icon"></i> -->
      <svg
        viewBox="0 0 24 24"
        fill="none"
        stroke="currentColor"
        stroke-width="1.5"
        stroke-linecap="round"
        stroke-linejoin="round"
        class="lucide lucide-shield-user-icon lucide-shield-user icon"
      >
        <path
          d="M20 13c0 5-3.5 7.5-7.66 8.95a1 1 0 0 1-.67-.01C7.5 20.5 4 18 4 13V6a1 1 0 0 1 1-1c2 0 4.5-1.2 6.24-2.72a1.17 1.17 0 0 1 1.52 0C14.51 3.81 17 5 19 5a1 1 0 0 1 1 1z"
        />
        <path d="M6.376 18.91a6 6 0 0 1 11.249.003" />
        <circle cx="12" cy="11" r="4" />
      </svg>
      <h3>Administrator</h3>
    </a>
    <a href="../LoginDept.aspx?type=Department" class="card">
      <!-- <i class="fa-solid fa-users-gear icon"></i> -->
      <svg viewBox="0 0 64 64" class="icon" fill="currentColor">
        <g>
          <path
            d="M41 25a1 1 0 0 0-1-1H24a1 1 0 0 0-1 1v4a1 1 0 0 0 1 1h16a1 1 0 0 0 1-1zm-2 3H25v-2h14zM41 35a1 1 0 0 0-1-1H30a1 1 0 1 0 0 2h9v2h-9a1 1 0 1 0 0 2h10a1 1 0 0 0 1-1zM30 48a1 1 0 1 0 0 2h10a1 1 0 0 0 1-1v-4a1 1 0 0 0-1-1H30a1 1 0 1 0 0 2h9v2z"
          ></path>
          <path
            d="M58 62h-3V1a1 1 0 0 0-1-1H36a1 1 0 0 0-1 1v10a1 1 0 1 0 2 0V2h16v60h-8V19a1 1 0 0 0-1-1h-7v-3a1 1 0 0 0-1-1h-3V9a1 1 0 1 0-2 0v5h-3a1 1 0 0 0-1 1v3h-7a1 1 0 0 0-1 1v10a1 1 0 1 0 2 0v-9h7a1 1 0 0 0 1-1v-3h6v3a1 1 0 0 0 1 1h7v42H27V35c0-.459-.313-.858-.758-.97l-16-4A1 1 0 0 0 9 31v31H6a1 1 0 1 0 0 2h52a1 1 0 1 0 0-2zM25 35.781V62h-6V38a1 1 0 0 0-1-1h-7v-4.719zM11 39h6v23h-6z"
          ></path>
          <path
            d="M40 10h10a1 1 0 0 0 1-1V5a1 1 0 0 0-1-1H40a1 1 0 0 0-1 1v4a1 1 0 0 0 1 1zm1-4h8v2h-8zM40 16h9v2h-1a1 1 0 1 0 0 2h2a1 1 0 0 0 1-1v-4a1 1 0 0 0-1-1H40a1 1 0 1 0 0 2zM51 25a1 1 0 0 0-1-1h-2a1 1 0 1 0 0 2h1v2h-1a1 1 0 1 0 0 2h2a1 1 0 0 0 1-1zM51 35a1 1 0 0 0-1-1h-2a1 1 0 1 0 0 2h1v2h-1a1 1 0 1 0 0 2h2a1 1 0 0 0 1-1zM48 48a1 1 0 1 0 0 2h2a1 1 0 0 0 1-1v-4a1 1 0 0 0-1-1h-2a1 1 0 1 0 0 2h1v2z"
          ></path>
        </g>
      </svg>

      <h3>Departments</h3>
    </a>
      <a href="../DivisionLogin.aspx?type=Division" class="card">

    <svg
        viewBox="0 0 24 24"
        fill="none"
        stroke="currentColor"
        stroke-width="1.5"
        stroke-linecap="round"
        stroke-linejoin="round"
        class="icon">

        <!-- Building -->
        <path d="M3 21h18"/>
        <path d="M5 21V7l7-4 7 4v14"/>
        <path d="M9 21v-4h6v4"/>
        <path d="M9 9h.01"/>
        <path d="M12 9h.01"/>
        <path d="M15 9h.01"/>
        <path d="M9 13h.01"/>
        <path d="M12 13h.01"/>
        <path d="M15 13h.01"/>
    </svg>

    <h3>Divisions</h3>

</a>
    <a
      href="../DistrictLogin.aspx?type=Districts"
      class="card"
    >
      <!-- <i class="fa-solid fa-house-user icon"></i> -->
      <svg
        viewBox="0 0 24 24"
        fill="none"
        stroke="currentColor"
        stroke-width="1.5"
        stroke-linecap="round"
        stroke-linejoin="round"
        class="lucide lucide-map-pinned-icon lucide-map-pinned icon"
      >
        <path
          d="M18 8c0 3.613-3.869 7.429-5.393 8.795a1 1 0 0 1-1.214 0C9.87 15.429 6 11.613 6 8a6 6 0 0 1 12 0"
        />
        <circle cx="12" cy="8" r="2" />
        <path
          d="M8.714 14h-3.71a1 1 0 0 0-.948.683l-2.004 6A1 1 0 0 0 3 22h18a1 1 0 0 0 .948-1.316l-2-6a1 1 0 0 0-.949-.684h-3.712"
        />
      </svg>
      <h3>Districts</h3>
    </a>
  </section>
</main>
</asp:Content>


