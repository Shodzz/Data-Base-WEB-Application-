using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace YourNamespace
{
    public partial class AnnualLeave : System.Web.UI.Page
    {
        private int employeeID;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["EmployeeID"] != null)
            {
                employeeID = (int)Session["EmployeeID"];
            }
            else
            {
                lblMessage.Text = "No Employee ID provided. Please log in again.";
                lblMessage.ForeColor = System.Drawing.Color.Red;
            }
        }

        protected void btnSubmitLeave_Click(object sender, EventArgs e)
        {
            if (employeeID == 0)
            {
                lblMessage.Text = "Invalid Employee ID.";
                lblMessage.ForeColor = System.Drawing.Color.Red;
                return;
            }

            if (!int.TryParse(txtReplacementEmp.Text.Trim(), out int replacementEmp))
            {
                lblMessage.Text = "Please enter a valid Replacement Employee ID.";
                lblMessage.ForeColor = System.Drawing.Color.Red;
                return;
            }

            if (!DateTime.TryParse(txtStartDate.Text.Trim(), out DateTime startDate) ||
                !DateTime.TryParse(txtEndDate.Text.Trim(), out DateTime endDate))
            {
                lblMessage.Text = "Please enter valid start and end dates.";
                lblMessage.ForeColor = System.Drawing.Color.Red;
                return;
            }

            string connString = ConfigurationManager.ConnectionStrings["MyDatabaseConnection"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connString))
            {
                try
                {
                    conn.Open();

                    using (SqlCommand checkCmd = new SqlCommand("SELECT COUNT(*) FROM Employee WHERE Employee_ID = @replacementEmp", conn))
                    {
                        checkCmd.Parameters.Add("@replacementEmp", SqlDbType.Int).Value = replacementEmp;

                        int exists = (int)checkCmd.ExecuteScalar();
                        if (exists == 0)
                        {
                            lblMessage.Text = "Replacement Employee ID does not exist in the database.";
                            lblMessage.ForeColor = System.Drawing.Color.Red;
                            return;
                        }
                    }

                    using (SqlCommand cmd = new SqlCommand("Submit_annual", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@employee_ID", SqlDbType.Int).Value = employeeID;
                        cmd.Parameters.Add("@replacement_emp", SqlDbType.Int).Value = replacementEmp;
                        cmd.Parameters.Add("@start_date", SqlDbType.Date).Value = startDate;
                        cmd.Parameters.Add("@end_date", SqlDbType.Date).Value = endDate;

                        cmd.ExecuteNonQuery();

                        lblMessage.Text = "Annual leave request submitted successfully.";
                        lblMessage.ForeColor = System.Drawing.Color.Green;
                    }
                }
                catch (Exception ex)
                {
                    lblMessage.Text = "Database error: " + ex.Message;
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                }
            }
        }
    }
}