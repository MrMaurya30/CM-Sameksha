<%@ Page Title="" Language="C#" MasterPageFile="~/Department/MasterDept.master" AutoEventWireup="true" CodeFile="DeptResponse.aspx.cs" Inherits="Department_DeptResponse" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<style>
    .response-card {
        background: #ffffff;
        max-width: 800px;
        margin: 30px auto;
        padding: 25px 30px;
        border-radius: 14px;
        box-shadow: 0 10px 25px rgba(0,0,0,0.15);
    }

    .response-card h2 {
        text-align: center;
        color: #0d6efd;
        margin-bottom: 25px;
        font-weight: 600;
    }

    .response-card label {
        font-weight: 600;
        font-size: 14px;
        color: #333;
        margin-bottom: 6px;
        display: block;
    }

    .response-card .row {
        margin-bottom: 18px;
    }

    .response-card .form-control {
        border-radius: 8px;
        border: 1px solid #ced4da;
        padding: 10px 12px;
        font-size: 14px;
        box-shadow: none;
        transition: 0.3s;
    }

    .response-card .form-control:focus {
        border-color: #0d6efd;
        box-shadow: 0 0 0 0.15rem rgba(13,110,253,.25);
    }

    .response-card textarea.form-control {
        resize: none;
    }

    .response-card input[type="file"] {
        padding: 6px;
    }

    /* BUTTONS */
    .response-card .btn {
        width: 100%;
        padding: 10px;
        font-size: 15px;
        border-radius: 8px;
        font-weight: 500;
    }

    .response-card .btn-warning {
        background: #ffc107;
        border: none;
        color: #000;
    }

    .response-card .btn-primary {
        background: linear-gradient(90deg, #0d6efd, #0047ab);
        border: none;
    }

    .response-card .btn-primary:hover {
        opacity: 0.9;
    }

    /* MOBILE */
    @media (max-width: 576px) {
        .response-card {
            padding: 20px;
        }
    }
</style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="response-card">
    <h2>Issue Response</h2>
    <div class="row">
        <div class="col-sm-6">
                <label>Issue</label>
    <asp:TextBox runat="server" ID="textissue" CssClass="form-control"></asp:TextBox>
        </div>
        <div class="col-sm-6">
              <label>Your Name</label>
             <asp:TextBox runat="server" ID="TexBox1" CssClass="form-control" placeholder="Enter Your Name"></asp:TextBox>
        </div>
    </div> 
    <!-- File Upload -->
    <div class="row">
        <div class="col-sm-3">
            <label>Upload File</label>
             <asp:FileUpload  runat="server" CssClass="form-control"/>
        </div>
        <div class="col-sm-9">
                <label>Your Response</label>          
             <textarea  rows="4" runat="server" class="form-control" placeholder="Write your response here..."></textarea>
        </div>
    </div>
    <div class="row">
         <div class="col-sm-6"><asp:Button runat="server" Text="Cancel" CssClass="btn btn-warning" /></div>
         <div class="col-sm-6"><asp:Button runat="server" Text="Submit Response" CssClass="btn btn-primary" /></div>
    </div>
    
</div>

</asp:Content>

