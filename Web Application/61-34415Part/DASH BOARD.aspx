<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DASH BOARD.aspx.cs" Inherits="YourNamespace.Dashboard" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Employee Dashboard</title>
    <style>
        html, body { height: 100%; }
        body {
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
            background: linear-gradient(135deg, #4caf50 0%, #2196f3 50%, #0d47a1 100%);
            margin: 0;
            display: flex;
            justify-content: center;
            align-items: center;
        }
        .dashboard-card {
            background: #fff;
            padding: 40px;
            border-radius: 12px;
            box-shadow: 0 8px 20px rgba(0,0,0,0.2);
            width: 400px;
            text-align: center;
        }
        .dashboard-card h2 {
            margin-bottom: 20px;
            font-weight: bold;
            color: #0d47a1;
        }
        .dashboard-card input[type="submit"] {
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
            transition: filter 0.2s ease;
        }
        .dashboard-card input[type="submit"]:hover {
            filter: brightness(1.1);
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="dashboard-card">
            <h2>Welcome to Your Dashboard</h2>

            <asp:Button ID="btnPerformance" runat="server" Text="My Performance" OnClick="btnPerformance_Click" />
            <asp:Button ID="btnAttendance" runat="server" Text="My Attendance This Month" OnClick="btnAttendance_Click" />
            <asp:Button ID="btnPayroll" runat="server" Text="Last Month Payroll" OnClick="btnPayroll_Click" />
            <asp:Button ID="btnMyDeduction" runat="server" Text="My Deduction in a Certain Month" OnClick="btnMyDeduction_Click" />
            <asp:Button ID="btnLeaveStatus" runat="server" Text="Status of My Annual/Acc Leaves This Month" OnClick="btnLeaveStatus_Click" />
            <asp:Button ID="btnLeaveDashboard" runat="server" Text="Leave Dashboard" OnClick="btnLeaveDashboard_Click" />
            <asp:Button ID="btnUBApproveAnnualLeave" runat="server" Text="Approve Annual Leave" OnClick="btnUBApproveAnnualLeave_Click" />
            <asp:Button ID="btnUBApproveUnpaidLeave" runat="server" Text="Approve Unpaid Leave" OnClick="btnUBApproveUnpaidLeave_Click" />
            <asp:Button ID="btnUBEvaluateEmployee" runat="server" Text="Evaluate Employee" OnClick="btnUBEvaluateEmployee_Click" />

        </div>
    </form>
</body>
</html>
