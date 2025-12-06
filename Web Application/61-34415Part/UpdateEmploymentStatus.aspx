<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UpdateEmploymentStatus.aspx.cs" Inherits="HR.UpdateEmploymentStatus" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Update Employment Status</title>
    <style>
        body {
            font-family: 'Segoe UI', Arial, sans-serif;
            background: linear-gradient(135deg, #6a11cb, #2575fc);
            display: flex;
            justify-content: center;
            align-items: center;
            height: 100vh;
            margin: 0;
        }

        .container {
            background-color: #fff;
            padding: 40px;
            border-radius: 12px;
            box-shadow: 0px 6px 15px rgba(0,0,0,0.25);
            width: 420px;
            text-align: center;
        }

        h2 {
            margin-bottom: 25px;
            color: #2c3e50;
        }

        .input-box {
            margin-bottom: 15px;
        }

        .input-box input {
            width: 100%;
            padding: 10px;
            font-size: 14px;
            border-radius: 6px;
            border: 1px solid #ccc;
        }

        .btn-action {
            background: linear-gradient(135deg, #ff512f, #dd2476);
            color: #fff;
            border: none;
            padding: 12px;
            width: 100%;
            border-radius: 6px;
            font-size: 16px;
            cursor: pointer;
            margin-top: 10px;
            transition: transform 0.2s, opacity 0.3s;
        }

        .btn-action:hover {
            transform: scale(1.05);
            opacity: 0.9;
        }

        .message {
            font-weight: bold;
            margin-bottom: 15px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <h2>Update Employment Status</h2>

            <div class="input-box">
                <asp:TextBox ID="txtEmpID" runat="server" placeholder="Enter Employee ID" />
            </div>

            <asp:Label ID="lblMessage" runat="server" CssClass="message"></asp:Label>

            <asp:Button ID="btnUpdateStatus" runat="server" 
                        Text="Update Status" 
                        CssClass="btn-action" OnClick="btnUpdateStatus_Click" />

            <asp:Button ID="btnBack" runat="server" Text="Back to Dashboard" 
                        CssClass="btn-action" OnClick="btnBack_Click" />
        </div>
    </form>
</body>
</html>
