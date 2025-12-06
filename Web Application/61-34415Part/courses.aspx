<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="courses.aspx.cs" Inherits="HR.courses" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Admin Dashboard</title>
    <style>
        body {
            font-family: 'Segoe UI', Arial, sans-serif;
            background: linear-gradient(135deg, #ff9966, #ff5e62); /* orange → red gradient */
            display: flex;
            justify-content: center;
            align-items: center;
            height: 100vh;
            margin: 0;
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

        .btn-dashboard {
            background: linear-gradient(135deg, #3498db, #2ecc71); /* blue → green gradient */
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

        .output {
            margin-top: 20px;
            text-align: left;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="dashboard-box">
            <h2>Admin Dashboard</h2>

            <!-- Existing 7 buttons -->
            <asp:Button ID="btnViewEmployees" runat="server" Text="View All Employee Profiles" CssClass="btn-dashboard" OnClick="ViewEmployees_Click" />
            <asp:Button ID="btnEmpPerDept" runat="server" Text="Employees Per Department" CssClass="btn-dashboard" OnClick="EmployeesPerDept_Click" />
            <asp:Button ID="btnRejectedMedical" runat="server" Text="Rejected Medical Leaves" CssClass="btn-dashboard" OnClick="RejectedMedical_Click" />
            <asp:Button ID="btnRemoveDeductions" runat="server" Text="Remove Deductions of Resigned Employees" CssClass="btn-dashboard" OnClick="RemoveDeductions_Click" />
            <asp:Button ID="btnUpdateAttendance" runat="server" Text="Update Attendance for Employee" CssClass="btn-dashboard" OnClick="UpdateAttendance_Click" />
            <asp:Button ID="btnAddHoliday" runat="server" Text="Add New Official Holiday" CssClass="btn-dashboard" OnClick="AddHoliday_Click" />
            <asp:Button ID="btnInitiateAttendance" runat="server" Text="Initiate Attendance Records Today" CssClass="btn-dashboard" OnClick="InitiateAttendance_Click" />

            <!-- New 7 admin operation buttons -->
            <asp:Button ID="btnFetchYesterdayAttendance" runat="server" Text="Fetch Yesterday's Attendance" CssClass="btn-dashboard" OnClick="FetchYesterdayAttendance_Click" />
            <asp:Button ID="btnFetchWinterPerformance" runat="server" Text="Fetch Winter Performance" CssClass="btn-dashboard" OnClick="FetchWinterPerformance_Click" />
            <asp:Button ID="btnRemoveHolidayAttendance" runat="server" Text="Remove Holiday Attendance Records" CssClass="btn-dashboard" OnClick="RemoveHolidayAttendance_Click" />
            <asp:Button ID="btnRemoveUnattendedDayoff" runat="server" Text="Remove Unattended Dayoff" CssClass="btn-dashboard" OnClick="RemoveUnattendedDayoff_Click" />
            <asp:Button ID="btnRemoveApprovedLeaves" runat="server" Text="Remove Approved Leaves" CssClass="btn-dashboard" OnClick="RemoveApprovedLeaves_Click" />
            <asp:Button ID="btnReplaceEmployee" runat="server" Text="Replace Employee" CssClass="btn-dashboard" OnClick="ReplaceEmployee_Click" />
            <asp:Button ID="btnUpdateEmploymentStatus" runat="server" Text="Update Employment Status" CssClass="btn-dashboard" OnClick="UpdateEmploymentStatus_Click" />

            <div class="output">
                <asp:PlaceHolder ID="phOutput" runat="server"></asp:PlaceHolder>
            </div>
        </div>
    </form>
</body>

</html>
