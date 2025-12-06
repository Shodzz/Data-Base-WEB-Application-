<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DeductUnpaidLeave.aspx.cs" Inherits="HR.DeductUnpaidLeave" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Deduct Unpaid Leave</title>
    <style>
        .btn-action {
            background: linear-gradient(135deg, #ff512f, #dd2476);
            color: #fff;
            border: none;
            padding: 12px;
            width: 100%;
            border-radius: 6px;
            font-size: 16px;
            cursor: pointer;
            margin-top: 15px;
            transition: transform 0.2s, opacity 0.3s;
        }

            .btn-action:hover {
                transform: scale(1.05);
                opacity: 0.9;
            }

        body {
            font-family: 'Segoe UI', Arial, sans-serif;
            background: linear-gradient(135deg, #f7971e, #ffd200); /* orange → yellow gradient */
            margin: 0;
            height: 100vh;
            display: flex;
            justify-content: center;
            align-items: center;
        }

        .dashboard-box {
            background-color: #fff;
            padding: 40px;
            border-radius: 12px;
            box-shadow: 0px 6px 15px rgba(0,0,0,0.25);
            width: 450px;
            text-align: center;
        }

            .dashboard-box h2 {
                margin-bottom: 25px;
                color: #2c3e50;
            }

        .form-label {
            font-weight: bold;
            margin-right: 10px;
        }

        .textbox {
            width: 100%;
            padding: 10px;
            border: 1px solid #ccc;
            border-radius: 6px;
            margin-bottom: 20px;
            font-size: 14px;
        }

        .btn-dashboard {
            background: linear-gradient(135deg, #3498db, #2ecc71);
            color: #fff;
            border: none;
            padding: 12px;
            width: 100%;
            border-radius: 6px;
            font-size: 15px;
            cursor: pointer;
            transition: transform 0.2s, opacity 0.3s;
        }

            .btn-dashboard:hover {
                transform: scale(1.05);
                opacity: 0.9;
            }

        .error-message {
            color: red;
            font-weight: bold;
            margin-top: 15px;
            display: block;
        }

        .success-message {
            color: green;
            font-weight: bold;
            margin-top: 15px;
            display: block;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="dashboard-box">
            <h2>Add Deduction for Unpaid Leave</h2>

            <asp:Label Text="Employee ID:" CssClass="form-label" runat="server" />
            <asp:TextBox ID="txtEmpID" runat="server" CssClass="textbox" /><br />

            <asp:Button ID="btnSubmit" runat="server" Text="Apply Deduction" OnClick="btnSubmit_Click" CssClass="btn-dashboard" /><br />

            <asp:Label ID="lblMessage" runat="server" CssClass="error-message" Visible="False" />

            <!-- Return button -->
            <asp:Button ID="btnBack" runat="server" Text="Back to Dashboard"
                CssClass="btn-action" OnClick="btnBack_Click" />
        </div>
    </form>
</body>
</html>
