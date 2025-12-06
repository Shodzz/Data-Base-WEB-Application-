<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HRDashboard.aspx.cs" Inherits="HR.HRDashboard" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>HR Dashboard</title>
    <style>
        body {
            font-family: 'Segoe UI', Arial, sans-serif;
            background: linear-gradient(135deg, #1d976c, #93f9b9);
            margin: 0;
            height: 100vh;
            display: flex;
        }

        .sidebar {
            background-color: #2c3e50;
            color: #fff;
            width: 250px;
            padding: 30px;
            box-shadow: 2px 0 8px rgba(0,0,0,0.2);
        }

        .sidebar h3 {
            margin-bottom: 20px;
            border-bottom: 1px solid #444;
            padding-bottom: 10px;
        }

        .sidebar p {
            margin: 10px 0;
            font-size: 14px;
        }

        .dashboard-box {
            flex: 1;
            background-color: #fff;
            margin: 40px;
            padding: 40px;
            border-radius: 12px;
            box-shadow: 0px 6px 15px rgba(0,0,0,0.25);
            text-align: center;
        }

        .dashboard-box h2 {
            margin-bottom: 25px;
            color: #2c3e50;
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
            margin-bottom: 15px;
            transition: transform 0.2s, opacity 0.3s;
        }

        .btn-dashboard:hover {
            transform: scale(1.05);
            opacity: 0.9;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <!-- Sidebar with HR info -->
        <div class="sidebar">
            <h3>HR Info</h3>
            <p><strong>ID:</strong> <asp:Label ID="lblHRID" runat="server" /></p>
            <p><strong>Name:</strong> <asp:Label ID="lblHRName" runat="server" /></p>
            <p><strong>Department:</strong> <asp:Label ID="lblHRDept" runat="server" /></p>
        </div>

        <!-- Main dashboard -->
        <div class="dashboard-box">
            <h2>HR Dashboard</h2>

            <asp:Button ID="btnAnnualAccidental" runat="server" Text="Approve Annual/Accidental Leaves" PostBackUrl="ApproveAnnualAccidental.aspx" CssClass="btn-dashboard" />
            <asp:Button ID="btnUnpaid" runat="server" Text="Approve Unpaid Leaves" PostBackUrl="ApproveUnpaidLeave.aspx" CssClass="btn-dashboard" />
            <asp:Button ID="btnCompensation" runat="server" Text="Approve Compensation Leaves" PostBackUrl="ApproveCompensationLeave.aspx" CssClass="btn-dashboard" />
            <asp:Button ID="btnHoursDeduction" runat="server" Text="Deduct Missing Hours" PostBackUrl="DeductMissingHours.aspx" CssClass="btn-dashboard" />
            <asp:Button ID="btnDaysDeduction" runat="server" Text="Deduct Missing Days" PostBackUrl="DeductMissingDays.aspx" CssClass="btn-dashboard" />
            <asp:Button ID="btnUnpaidLeaveDeduction" runat="server" Text="Deduct Unpaid Leave" PostBackUrl="DeductUnpaidLeave.aspx" CssClass="btn-dashboard" />
            <asp:Button ID="btnPayroll" runat="server" Text="Generate Monthly Payroll" PostBackUrl="GeneratePayroll.aspx" CssClass="btn-dashboard" />
        </div>
    </form>
</body>
</html>
