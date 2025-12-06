<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FetchYesterdayAttendance.aspx.cs" Inherits="HR.FetchYesterdayAttendance" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Fetch Yesterday's Attendance</title>
    <style>
        body {
            font-family: 'Segoe UI', Arial, sans-serif;
            background: linear-gradient(135deg, #6a11cb, #2575fc); /* purple → blue gradient */
            display: flex;
            justify-content: center;
            align-items: flex-start;
            min-height: 100vh;
            margin: 0;
            padding: 40px 0;
        }

        .container {
            width: 85%;
            background-color: #fff;
            padding: 40px;
            border-radius: 12px;
            box-shadow: 0px 6px 15px rgba(0,0,0,0.25);
        }

        h2 {
            text-align: center;
            margin-bottom: 25px;
            color: #2c3e50;
        }

        .gridview {
            width: 100%;
            border-collapse: collapse;
            margin-top: 20px;
            font-size: 14px;
        }

        .gridview th {
            background: linear-gradient(135deg, #3498db, #2ecc71); /* blue → green gradient */
            color: #fff;
            padding: 12px;
            text-align: left;
        }

        .gridview td {
            padding: 12px;
            border-bottom: 1px solid #ddd;
        }

        .gridview tr:nth-child(even) {
            background-color: #f9f9f9;
        }

        .gridview tr:hover {
            background-color: #f1f1f1;
        }

        .btn {
            background: linear-gradient(135deg, #ff512f, #dd2476);
            color: #fff;
            border: none;
            padding: 12px 20px;
            border-radius: 6px;
            font-size: 15px;
            cursor: pointer;
            margin-top: 20px;
            transition: transform 0.2s, opacity 0.3s;
        }

        .btn:hover {
            transform: scale(1.05);
            opacity: 0.9;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <h2>Yesterday's Attendance Records</h2>

            <asp:GridView ID="gvYesterdayAttendance" runat="server" CssClass="gridview" AutoGenerateColumns="true" />

            <asp:Button ID="btnBack" runat="server" Text="Back to Dashboard" CssClass="btn" OnClick="btnBack_Click" />
        </div>
    </form>
</body>
</html>
