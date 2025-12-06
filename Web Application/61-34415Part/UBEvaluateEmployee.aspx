<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UBEvaluateEmployee.aspx.cs" Inherits="_61_34415Part.UBEvaluateEmployee" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title>Evaluate Employees</title>
    <style>
        body {
            font-family: Arial, sans-serif;
            background: linear-gradient(to right, #667eea, #764ba2);
            margin: 0;
            padding: 0;
        }

        .container {
            width: 900px;
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

        .eval-btn {
            background: #667eea;
            color: white;
            border: none;
            padding: 8px 15px;
            border-radius: 6px;
            cursor: pointer;
            transition: 0.3s;
        }

        .eval-btn:hover {
            background: #4c5bd4;
        }

        input[type="text"] {
            width: 100%;
            padding: 5px 8px;
            border-radius: 6px;
            border: 1px solid #ccc;
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
            <h2>Evaluate Employees in Your Department</h2>
            <div class="gridview-container">
                <asp:GridView ID="gvEmployees" runat="server" AutoGenerateColumns="false" CssClass="styled-grid"
                    OnRowCommand="gvEmployees_RowCommand">
                    <Columns>
                        <asp:BoundField DataField="employee_id" HeaderText="Employee ID" />
                        <asp:BoundField DataField="employee_name" HeaderText="Employee Name" />
                        <asp:TemplateField HeaderText="Rating (1-5)">
                            <ItemTemplate>
                                <asp:TextBox ID="txtRating" runat="server" Width="50" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Comments">
                            <ItemTemplate>
                                <asp:TextBox ID="txtComments" runat="server" Width="200" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Semester (e.g., W23)">
                            <ItemTemplate>
                                <asp:TextBox ID="txtSemester" runat="server" Width="50" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:Button ID="btnSubmitEvaluation" runat="server" CssClass="eval-btn" Text="Submit Evaluation"
                                    CommandName="Evaluate" CommandArgument='<%# Eval("employee_id") %>' />
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
