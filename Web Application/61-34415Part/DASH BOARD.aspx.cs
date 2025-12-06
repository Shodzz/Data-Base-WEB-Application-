using System;
using System.Configuration;
using System.Data.SqlClient;

namespace YourNamespace
{
    public partial class Dashboard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Block if not logged in
                if (Session["EmployeeID"] == null)
                {
                    Response.Redirect("LOGIN PAGE.aspx");
                    return;
                }

                int employeeId = Convert.ToInt32(Session["EmployeeID"]);
                string role = GetEmployeeRole(employeeId);

                // Hide all protected buttons first
                btnUBApproveAnnualLeave.Visible = false;
                btnUBApproveUnpaidLeave.Visible = false;
                btnUBEvaluateEmployee.Visible = false;

                // Annual + Unpaid ONLY IF IAM Dean, Vice Dean, President
                if (role == "Dean" || role == "Vice Dean" || role == "President")
                {
                    btnUBApproveAnnualLeave.Visible = true;
                    btnUBApproveUnpaidLeave.Visible = true;
                }

                // Evaluate ONLY IF IAM Dean
                if (role == "Dean")
                {
                    btnUBEvaluateEmployee.Visible = true;
                }
            }
        }

        private string GetEmployeeRole(int employeeId)
        {
            string role = "";

            string cs = ConfigurationManager.ConnectionStrings["MyDatabaseConnection"].ConnectionString;

            using (SqlConnection con = new SqlConnection(cs))
            {

                string query = "SELECT TOP 1 r.role_name FROM Employee_Role er inner join Role r on er.role_name=r.role_name where er.emp_ID=@id ORDER BY r.rank asc";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@id", employeeId);

                con.Open();
                object result = cmd.ExecuteScalar();

                if (result != null)
                    role = result.ToString();
            }

            return role;
        }

        protected void btnPerformance_Click(object sender, EventArgs e)
        {
            Response.Redirect("Performance.aspx");
        }

        protected void btnAttendance_Click(object sender, EventArgs e)
        {
            Response.Redirect("MyAttendance.aspx");
        }

        protected void btnPayroll_Click(object sender, EventArgs e)
        {
            Response.Redirect("LastMonthPayroll.aspx");
        }

        protected void btnMyDeduction_Click(object sender, EventArgs e)
        {
            Response.Redirect("MyDeductions.aspx");
        }

        protected void btnLeaveStatus_Click(object sender, EventArgs e)
        {
            Response.Redirect("AnnualAccidentalLeavesStatus.aspx");
        }

        protected void btnLeaveDashboard_Click(object sender, EventArgs e)
        {
            Response.Redirect("LeaveDashboard.aspx");
        }

        protected void btnUBApproveAnnualLeave_Click(object sender, EventArgs e)
        {
            Response.Redirect("UBApproveAnnualLeave.aspx");
        }

        protected void btnUBApproveUnpaidLeave_Click(object sender, EventArgs e)
        {
            Response.Redirect("UBApproveUnpaidLeave.aspx");
        }

        protected void btnUBEvaluateEmployee_Click(object sender, EventArgs e)
        {
            Response.Redirect("UBEvaluateEmployee.aspx");
        }
    }
}
