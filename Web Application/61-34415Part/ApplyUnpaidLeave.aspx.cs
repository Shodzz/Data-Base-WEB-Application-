using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace _61_34415Part
{
    public partial class ApplyUnpaidLeave : System.Web.UI.Page
    {
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            lblMessage.ForeColor = System.Drawing.Color.Red;

            //int empId = 15; // Hardcoded for testing
            int empId = Convert.ToInt32(Session["EmployeeID"]);

            if (!DateTime.TryParse(txtStartDate.Text, out DateTime startDate))
            {
                lblMessage.Text = "Error: Please enter a valid Start Date.";
                return;
            }

            if (!DateTime.TryParse(txtEndDate.Text, out DateTime endDate))
            {
                lblMessage.Text = "Error: Please enter a valid End Date.";
                return;
            }

            if (!EmployeeExists(empId))
            {
                lblMessage.Text = "Error: Employee ID does not exist in the database.";
                return;
            }

            string connStr = ConfigurationManager.ConnectionStrings["MyDatabaseConnection"].ConnectionString;

            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                using (SqlCommand cmd = new SqlCommand("Submit_unpaid", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@employee_ID", empId);
                    cmd.Parameters.AddWithValue("@start_date", startDate);
                    cmd.Parameters.AddWithValue("@end_date", endDate);
                    cmd.Parameters.AddWithValue("@document_description", txtDocumentDescription.Text);
                    cmd.Parameters.AddWithValue("@file_name", txtFileName.Text);

                    conn.Open();
                    cmd.ExecuteNonQuery();

                    lblMessage.ForeColor = System.Drawing.Color.Green;
                    lblMessage.Text = "Unpaid leave submitted successfully!";
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Error submitting unpaid leave: " + ex.Message;
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
