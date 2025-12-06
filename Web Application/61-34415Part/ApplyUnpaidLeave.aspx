<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ApplyUnpaidLeave.aspx.cs" Inherits="_61_34415Part.ApplyUnpaidLeave" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title>Apply for Unpaid Leave</title>
    <style>
        body {
            margin: 0;
            min-height: 100vh;
            font-family: "Segoe UI", Tahoma, sans-serif;
            background: linear-gradient(135deg, #667eea, #764ba2);
            display: flex;
            align-items: center;
            justify-content: center;
        }
        .leave-container {
            background: #fff;
            width: 420px;
            padding: 30px;
            border-radius: 14px;
            box-shadow: 0 15px 35px rgba(0,0,0,0.25);
            animation: fadeIn 0.6s ease;
        }
        .leave-container h2 {
            text-align:center; margin-bottom:25px; color:#333;
        }
        .form-group { margin-bottom:18px; }
        .form-group label { display:block; margin-bottom:6px; color:#444; font-weight:600; }
        .form-group input { width:100%; padding:10px; border-radius:6px; border:1px solid #ccc; font-size:15px; }
        .submit-btn {
            width:100%; padding:12px; border:none; border-radius:8px;
            background: linear-gradient(90deg,#667eea,#764ba2); color:#fff; font-size:17px; font-weight:bold;
            cursor:pointer; transition:0.3s;
        }
        .submit-btn:hover { opacity:0.9; transform:translateY(-2px); }
        .message-label { display:block; margin-top:15px; text-align:center; font-weight:bold; }
    </style>
</head>
<body>
<form id="form1" runat="server">
    <div class="leave-container">
        <h2>Unpaid Leave Application</h2>

        <div class="form-group">
            <label>Start Date</label>
            <asp:TextBox ID="txtStartDate" runat="server" TextMode="Date" />
        </div>

        <div class="form-group">
            <label>End Date</label>
            <asp:TextBox ID="txtEndDate" runat="server" TextMode="Date" />
        </div>

        <div class="form-group">
            <label>Document Description</label>
            <asp:TextBox ID="txtDocumentDescription" runat="server" />
        </div>

        <div class="form-group">
            <label>File Name</label>
            <asp:TextBox ID="txtFileName" runat="server" />
        </div>

        <asp:Button ID="btnSubmit" runat="server" Text="Submit Unpaid Leave"
            CssClass="submit-btn" OnClick="btnSubmit_Click" />
        <asp:Label ID="lblMessage" runat="server" CssClass="message-label" />
    </div>
</form>
</body>
</html>
