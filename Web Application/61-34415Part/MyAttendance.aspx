<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MyAttendance.aspx.cs" Inherits="YourNamespace.MyAttendance" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>My Attendance</title>
    <style>
        body {
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
            background: linear-gradient(135deg, #4caf50 0%, #2196f3 50%, #0d47a1 100%);
            margin: 0;
            display: flex;
            justify-content: center;
            align-items: center;
            height: 100vh;
        }
        .attendance-card {
            background: #fff;
            padding: 40px;
            border-radius: 12px;
            box-shadow: 0 8px 20px rgba(0,0,0,0.2);
            width: 90%;
            max-width: 1200px;
            text-align: center;
        }
        .attendance-card h2 {
            margin-bottom: 20px;
            font-weight: bold;
            color: #0d47a1;
        }
        .theme-button {
            margin-top: 20px;
            width: 100%;
            padding: 12px;
            background: linear-gradient(90deg, #4caf50, #2196f3);
            border: none;
            border-radius: 6px;
            color: white;
            font-size: 16px;
            font-weight: bold;
            cursor: pointer;
        }
        .gridview {
            margin-top: 20px;
            border-collapse: collapse;
            width: 100%;
        }
        .gridview th, .gridview td {
            border: 1px solid #ccc;
            padding: 8px;
            text-align: center;
        }
        .gridview th {
            background-color: #2196f3;
            color: white;
        }
        /* Make the date column wider */
        .gridview th.date-col, .gridview td.date-col {
            width: 180px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="attendance-card">
            <h2>My Attendance This Month</h2>

            <asp:Button ID="btnShowAttendance" runat="server" Text="Load Attendance" CssClass="theme-button" OnClick="btnShowAttendance_Click" />

            <asp:Label ID="lblMessage" runat="server"></asp:Label>

            <!-- Auto-generate columns; format date via RowDataBound -->
            <asp:GridView ID="gvAttendance" runat="server" CssClass="gridview" AutoGenerateColumns="True"
                OnRowDataBound="gvAttendance_RowDataBound">
            </asp:GridView>
        </div>
    </form>
</body>
</html>
