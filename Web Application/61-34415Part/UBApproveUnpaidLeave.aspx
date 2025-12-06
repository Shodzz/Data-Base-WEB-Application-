<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UBApproveUnpaidLeave.aspx.cs" Inherits="_61_34415Part.UBApproveUnpaidLeave" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title>Approve Unpaid Leave</title>
    <style>
        body {
            font-family: Arial, sans-serif;
            background: linear-gradient(to right, #667eea, #764ba2);
            margin: 0;
            padding: 0;
        }

        .container {
            width: 800px;
            margin: 80px auto;
            background: white;
            border-radius: 12px;
            padding: 30px;
            box-shadow: 0px 10px 25px rgba(0,0,0,0.3);
        }

        h2 {
            color: #333;
            text-align: center;
            margin-bottom: 30px;
        }

        .gridview-container {
            overflow-x: auto;
        }

        .process-btn {
            background: #667eea;
            color: white;
            border: none;
            padding: 8px 15px;
            border-radius: 6px;
            cursor: pointer;
            transition: 0.3s;
        }

        .process-btn:hover {
            background: #4c5bd4;
        }

        #lblMessage {
            display: block;
            text-align: center;
            margin-top: 20px;
            font-weight: bold;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <h2>Pending Unpaid Leave Requests</h2>
            <div class="gridview-container">
                <asp:GridView ID="gvPendingRequests" runat="server" AutoGenerateColumns="false" CssClass="styled-grid"
                    OnRowCommand="gvPendingRequests_RowCommand">
                    <Columns>
                        <asp:BoundField DataField="request_ID" HeaderText="Request ID" />
                        <asp:BoundField DataField="employee_name" HeaderText="Employee Name" />
                        <asp:BoundField DataField="start_date" HeaderText="Start Date" DataFormatString="{0:yyyy-MM-dd}" />
                        <asp:BoundField DataField="end_date" HeaderText="End Date" DataFormatString="{0:yyyy-MM-dd}" />
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:Button ID="btnProcess" runat="server" CssClass="process-btn" Text="Process Request"
                                    CommandName="Process" CommandArgument='<%# Eval("request_ID") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
            <asp:Label ID="lblMessage" runat="server"></asp:Label>
        </div>
    </form>
</body>
</html>
