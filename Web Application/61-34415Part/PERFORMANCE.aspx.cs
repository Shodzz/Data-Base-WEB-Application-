using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace YourNamespace
{
    public partial class Performance : System.Web.UI.Page
    {
        private int employeeID;

        protected void Page_Load(object sender, EventArgs e)
        {
            // Retrieve EmployeeID from Session
            if (Session["EmployeeID"] != null)
            {
                employeeID = (int)Session["EmployeeID"];
            }
            else
            {
                lblMessage.Text = "No Employee ID provided. Please log in again.";
                lblMessage.ForeColor = System.Drawing.Color.Red;
                return;
            }
        }

        protected void btnShow_Click(object sender, EventArgs e)
        {
            string period = txtPeriod.Text.Trim().ToUpper(); // Optional: convert to uppercase

            // Validate that period is exactly 3 characters
            if (string.IsNullOrEmpty(period) || period.Length != 3)
            {
                lblMessage.Text = "Please enter a valid semester (e.g., W23).";
                lblMessage.ForeColor = System.Drawing.Color.Red;
                return;
            }

            string connString = ConfigurationManager.ConnectionStrings["MyDatabaseConnection"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connString))
            {
                try
                {
                    conn.Open();

                    // Call the table-valued function
                    using (SqlCommand cmd = new SqlCommand("SELECT * FROM dbo.MyPerformance(@employee_ID, @period)", conn))
                    {
                        cmd.Parameters.Add("@employee_ID", SqlDbType.Int).Value = employeeID;
                        cmd.Parameters.Add("@period", SqlDbType.Char, 3).Value = period;

                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        gvPerformance.DataSource = dt;
                        gvPerformance.DataBind();

                        if (dt.Rows.Count > 0)
                        {
                            lblMessage.Text = "Performance records loaded.";
                            lblMessage.ForeColor = System.Drawing.Color.Green;
                        }
                        else
                        {
                            lblMessage.Text = "No records found for the given semester.";
                            lblMessage.ForeColor = System.Drawing.Color.Red;
                        }
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
