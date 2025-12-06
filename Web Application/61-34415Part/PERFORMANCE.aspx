<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Performance.aspx.cs" Inherits="YourNamespace.Performance" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Employee Performance</title>
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
        .performance-card {
            background: #fff;
            padding: 40px;
            border-radius: 12px;
            box-shadow: 0 8px 20px rgba(0,0,0,0.2);
            width: 600px;
            text-align: center;
        }
        .performance-card h2 {
            margin-bottom: 20px;
            font-weight: bold;
            color: #0d47a1; 
        }
        .performance-card label {
            display: block;
            margin-top: 15px;
            font-weight: bold;
            color: #2196f3;
            text-align: left;
        }
        .performance-card input[type="text"] {
            width: 100%;
            padding: 10px;
            margin-top: 6px;
            border: 1px solid #c9d2e3;
            border-radius: 6px;
            font-size: 14px;
        }
        .performance-card input[type="submit"] {
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
        .performance-card input[type="submit"]:hover { filter: brightness(1.1); }
        .performance-card .message {
            margin-top: 15px;
            font-size: 14px;
            font-weight: bold;
            color: #0d47a1;
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
            font-weight: bold;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="performance-card">
            <h2>My Performance</h2>

            <asp:Label ID="lblPeriod" runat="server" Text="Semester (e.g. S01):"></asp:Label>
            <asp:TextBox ID="txtPeriod" runat="server"></asp:TextBox>

            <asp:Button ID="btnShow" runat="server" Text="Show Performance" OnClick="btnShow_Click" />

            <asp:Label ID="lblMessage" runat="server" CssClass="message"></asp:Label>

            <asp:GridView ID="gvPerformance" runat="server" CssClass="gridview" AutoGenerateColumns="true"></asp:GridView>
        </div>
    </form>
</body>
</html>
