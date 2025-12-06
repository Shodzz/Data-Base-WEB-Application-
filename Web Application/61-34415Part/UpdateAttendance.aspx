<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UpdateAttendance.aspx.cs" Inherits="HR.UpdateAttendance" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Update Attendance</title>
    <style>
        body {
            font-family: 'Segoe UI', Arial, sans-serif;
            background: linear-gradient(135deg, #6a11cb, #2575fc); /* purple → blue gradient */
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
            width: 400px;
            text-align: center;
        }

        h2 {
            margin-bottom: 25px;
            color: #333;
        }

        .form-field {
            margin-bottom: 20px;
            text-align: left;
        }

        .form-field input {
            width: 100%;
            padding: 12px;
            border: 1px solid #ccc;
            border-radius: 6px;
            outline: none;
            transition: border-color 0.3s, box-shadow 0.3s;
        }

        .form-field input:focus {
            border-color: #2575fc;
            box-shadow: 0 0 5px rgba(37,117,252,0.5);
        }

        .btn-action {
            background: linear-gradient(135deg, #ff512f, #dd2476); /* orange → pink gradient */
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
            <asp:Label ID="lblMessage" runat="server" CssClass="message"></asp:Label>
            <h2>Update Attendance</h2>

            <div class="form-field">
                <asp:TextBox ID="txtEmployeeID" runat="server" Placeholder="Employee ID"></asp:TextBox>
            </div>

            <div class="form-field">
                <!-- Use TextMode="Time" for efficient time input -->
                <asp:TextBox ID="txtCheckIn" runat="server" TextMode="Time" />
            </div>

            <div class="form-field">
                <asp:TextBox ID="txtCheckOut" runat="server" TextMode="Time" />
            </div>

            <asp:Button ID="btnUpdate" runat="server" Text="Update Attendance" CssClass="btn-action" OnClick="btnUpdate_Click" />
            <asp:Button ID="btnBack" runat="server" Text="Back to Dashboard" CssClass="btn-action" OnClick="btnBack_Click" />
        </div>
    </form>
</body>
</html>
