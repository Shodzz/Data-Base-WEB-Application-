<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ApproveUnpaidLeave.aspx.cs" Inherits="HR.ApproveUnpaidLeave" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Approve Unpaid Leaves</title>
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
            background: linear-gradient(135deg, #11998e, #38ef7d); /* green gradient */
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
            width: 950px;
            text-align: center;
        }

            .dashboard-box h2 {
                margin-bottom: 25px;
                color: #2c3e50;
            }

        .gridview {
            width: 100%;
            border-collapse: collapse;
            margin-bottom: 20px;
        }

            .gridview th {
                background: #16a085;
                color: #fff;
                padding: 10px;
                text-align: center;
            }

            .gridview td {
                padding: 10px;
                border-bottom: 1px solid #ddd;
                text-align: center;
            }

            .gridview tr:nth-child(even) {
                background-color: #f9f9f9;
            }

            .gridview tr:hover {
                background-color: #f1f1f1;
            }

        .error-message {
            color: red;
            font-weight: bold;
        }

        .success-message {
            color: green;
            font-weight: bold;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="dashboard-box">
            <h2>Approve Unpaid Leaves</h2>

            <asp:GridView ID="gvLeaves" runat="server" AutoGenerateColumns="False"
                CssClass="gridview"
                DataKeyNames="request_ID"
                OnRowCommand="gvLeaves_RowCommand">
                <Columns>
                    <asp:BoundField DataField="request_ID" HeaderText="Request ID" />
                    <asp:BoundField DataField="emp_ID" HeaderText="Employee ID" />
                    <asp:BoundField DataField="num_days" HeaderText="Days Requested" />
                    <asp:BoundField DataField="start_date" HeaderText="Start Date" DataFormatString="{0:yyyy-MM-dd}" />
                    <asp:BoundField DataField="end_date" HeaderText="End Date" DataFormatString="{0:yyyy-MM-dd}" />
                    <asp:ButtonField Text="Process" CommandName="Process" ButtonType="Button" />
                </Columns>
            </asp:GridView>

            <asp:Label ID="lblMessage" runat="server" CssClass="error-message" Visible="False" />

            <!-- Return button -->
            <asp:Button ID="btnBack" runat="server" Text="Back to Dashboard"
                CssClass="btn-action" OnClick="btnBack_Click" />
        </div>
    </form>
</body>
</html>
