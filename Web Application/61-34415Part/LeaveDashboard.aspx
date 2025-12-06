<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LeaveDashboard.aspx.cs" Inherits="_61_34415Part.LeaveDashboard" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title>Leave Dashboard</title>

    <style>
        body {
            font-family: Arial;
            background: linear-gradient(to right, #667eea, #764ba2);
            margin: 0;
            padding: 0;
        }

        .container {
            width: 600px;
            margin: 100px auto;
            background: white;
            border-radius: 12px;
            padding: 30px;
            box-shadow: 0px 10px 25px rgba(0,0,0,0.3);
            text-align: center;
        }

        h2 {
            margin-bottom: 30px;
            color: #333;
        }

        .leave-btn {
            display: block;
            width: 80%;             
            margin: 0 auto 15px;    
            padding: 15px;
            background: #667eea;
            color: white;
            text-decoration: none;
            border-radius: 8px;
            font-size: 18px;
            transition: 0.3s;
            text-align: center;
        }

        .leave-btn:hover {
            background: #4c5bd4;
        }
    </style>

</head>
<body>
    <form id="form1" runat="server">

        <div class="container">
            <h2>Leave Application Dashboard</h2>

            <a class="leave-btn" href="ApplyAccidentalLeave.aspx">Apply for Accidental Leave</a>
            <a class="leave-btn" href="ApplyCompensationLeave.aspx">Apply for Compensation Leave</a>
            <a class="leave-btn" href="ApplyMedicalLeave.aspx">Apply for Medical Leave</a>
            <a class="leave-btn" href="ApplyUnpaidLeave.aspx">Apply for Unpaid Leave</a>
            <a class="leave-btn" href="AnnualLeave.aspx">Apply for Annual Leave</a>

        </div>

    </form>
</body>
</html>
