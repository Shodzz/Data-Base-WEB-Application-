using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace _61_34415Part
{
    public partial class ApplyCompensation : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            lblMessage.ForeColor = System.Drawing.Color.Red;

            //int empId = 15; // Hardcoded for testing
            int empId = Convert.ToInt32(Session["EmployeeID"]);

            if (!DateTime.TryParse(txtCompDate.Text, out DateTime compDate))
            {
                lblMessage.Text = "Error: Please enter a valid Compensation Date.";
                return;
            }

            if (!DateTime.TryParse(txtOriginalWorkday.Text, out DateTime originalWorkday))
            {
                lblMessage.Text = "Error: Please enter a valid Original Workday Date.";
                return;
            }

            if (!int.TryParse(txtRepEmpID.Text, out int repEmpId))
            {
                lblMessage.Text = "Error: Please enter a valid numeric Replacement Employee ID.";
                return;
            }

            if (string.IsNullOrWhiteSpace(txtReason.Text))
            {
                lblMessage.Text = "Error: Please enter a reason for the compensation leave.";
                return;
            }

            if (!EmployeeExists(empId))
            {
                lblMessage.Text = "Error: Employee ID does not exist in the database.";
                return;
            }

            if (!EmployeeExists(repEmpId))
            {
                lblMessage.Text = "Error: Replacement Employee ID does not exist in the database.";
                return;
            }

            try
            {
                string connString = ConfigurationManager.ConnectionStrings["MyDatabaseConnection"].ConnectionString;

                using (SqlConnection conn = new SqlConnection(connString))
                using (SqlCommand cmd = new SqlCommand("Submit_compensation", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@employee_ID", empId);
                    cmd.Parameters.AddWithValue("@compensation_date", compDate);
                    cmd.Parameters.AddWithValue("@reason", txtReason.Text);
                    cmd.Parameters.AddWithValue("@date_of_original_workday", originalWorkday);
                    cmd.Parameters.AddWithValue("@rep_emp_id", repEmpId);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }

                lblMessage.ForeColor = System.Drawing.Color.Green;
                lblMessage.Text = "Compensation leave submitted successfully!";
            }
            catch (Exception ex)
            {
                lblMessage.ForeColor = System.Drawing.Color.Red;
                lblMessage.Text = "Error submitting compensation leave: " + ex.Message;
            }
        }

        private bool EmployeeExists(int employeeId)
        {
            string connStr = ConfigurationManager.ConnectionStrings["MyDatabaseConnection"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            using (SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM Employee WHERE employee_id = @id", conn))
            {
                cmd.Parameters.AddWithValue("@id", employeeId);
                conn.Open();
                int count = (int)cmd.ExecuteScalar();
                return count > 0;
            }
        }
    }
}
