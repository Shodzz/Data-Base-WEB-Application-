using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace _61_34415Part
{
    public partial class ApplyMedical : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e) { }

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

            if (!int.TryParse(txtInsuranceStatus.Text.Trim(), out int insValue) || (insValue != 0 && insValue != 1))
            {
                lblMessage.Text = "Error: Insurance Status must be 1 or 0.";
                return;
            }

            bool insuranceStatus = (insValue == 1);

            if (!EmployeeExists(empId))
            {
                lblMessage.Text = "Error: Employee ID does not exist in the database.";
                return;
            }

            try
            {
                string connString = ConfigurationManager.ConnectionStrings["MyDatabaseConnection"].ConnectionString;
                using (SqlConnection conn = new SqlConnection(connString))
                using (SqlCommand cmd = new SqlCommand("Submit_medical", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@employee_ID", empId);
                    cmd.Parameters.AddWithValue("@start_date", startDate);
                    cmd.Parameters.AddWithValue("@end_date", endDate);
                    cmd.Parameters.AddWithValue("@medical_type", ddlMedicalType.SelectedValue);
                    cmd.Parameters.AddWithValue("@insurance_status", insuranceStatus);
                    cmd.Parameters.AddWithValue("@disability_details", txtDisabilityDetails.Text);
                    cmd.Parameters.AddWithValue("@document_description", txtDocDescription.Text);
                    cmd.Parameters.AddWithValue("@file_name", txtFileName.Text);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }

                lblMessage.ForeColor = System.Drawing.Color.Green;
                lblMessage.Text = "Medical leave submitted successfully!";
            }
            catch (Exception ex)
            {
                lblMessage.ForeColor = System.Drawing.Color.Red;
                lblMessage.Text = "Error submitting medical leave: " + ex.Message;
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
