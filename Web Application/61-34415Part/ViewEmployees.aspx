<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ViewEmployees.aspx.cs" Inherits="HR.ViewEmployees" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>All Employee Profiles</title>
    <style>
        body {
            font-family: 'Segoe UI', Arial, sans-serif;
            background: linear-gradient(135deg, #6a11cb, #2575fc); /* purple → blue gradient */
            display: flex;
            justify-content: center;
            align-items: center;
            min-height: 100vh;
            margin: 0;
        }

        .container {
            background-color: #fff;
            padding: 40px;
            border-radius: 12px;
            box-shadow: 0px 6px 15px rgba(0,0,0,0.25);
            width: 900px;
            text-align: center;
        }

        h2 {
            margin-bottom: 25px;
            color: #2c3e50;
        }

        .back-button {
            margin-bottom: 20px;
            padding: 12px 20px;
            font-size: 16px;
            border-radius: 6px;
            border: none;
            cursor: pointer;
            background: linear-gradient(135deg, #ff512f, #dd2476); /* orange → pink gradient */
            color: #fff;
            transition: transform 0.2s, opacity 0.3s;
        }

        .back-button:hover {
            transform: scale(1.05);
            opacity: 0.9;
        }

        /* GridView styling */
        .gv-container {
            overflow-x: auto;
        }

        #gvEmployees {
            width: 100%;
            border-collapse: collapse;
            margin-bottom: 20px;
            font-size: 14px;
        }

        #gvEmployees th, #gvEmployees td {
            padding: 12px;
            border: 1px solid #ddd;
            text-align: left;
        }

        #gvEmployees th {
            background: linear-gradient(135deg, #3498db, #2ecc71); /* blue → green gradient */
            color: #fff;
        }

        #gvEmployees tr:nth-child(even) {
            background-color: #f9f9f9;
        }

        #gvEmployees tr:hover {
            background-color: #f1f1f1;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <h2>All Employee Profiles</h2>

            <asp:Button ID="btnBack" runat="server" Text="Back to Dashboard" CssClass="back-button" OnClick="btnBack_Click" />

            <div class="gv-container">
                <asp:GridView ID="gvEmployees" runat="server" AutoGenerateColumns="true"></asp:GridView>
            </div>
        </div>
    </form>
</body>
</html>
