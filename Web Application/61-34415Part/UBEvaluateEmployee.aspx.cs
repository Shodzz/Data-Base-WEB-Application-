using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace _61_34415Part
{
    public partial class UBEvaluateEmployee : System.Web.UI.Page
    {
        private int LoggedInEmployeeID
        {
            get
            {
                if (Session["EmployeeID"] != null && int.TryParse(Session["EmployeeID"].ToString(), out int id))
                    return id;

                throw new InvalidOperationException("User not logged in.");

            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                LoadDepartmentEmployees(""); // empty semester initially
        }

        private void LoadDepartmentEmployees(string semester)
        {
            string connString = ConfigurationManager.ConnectionStrings["MyDatabaseConnection"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connString))
            using (SqlCommand cmd = new SqlCommand(@"
                SELECT employee_id, first_name + ' ' + last_name AS employee_name
                FROM Employee
                WHERE dept_name = (SELECT dept_name FROM Employee WHERE employee_id = @DeanID)
                  AND employee_id <> @DeanID
                  AND (@semester = '' OR employee_id NOT IN (
                        SELECT emp_ID FROM Performance WHERE semester = @semester
                  ))
                ORDER BY first_name, last_name", conn))
            {
                cmd.Parameters.AddWithValue("@DeanID", LoggedInEmployeeID);
                cmd.Parameters.AddWithValue("@semester", semester);

                conn.Open();
                DataTable dt = new DataTable();
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    da.Fill(dt);

                gvEmployees.DataSource = dt;
                gvEmployees.DataBind();
            }
        }

        protected void gvEmployees_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Evaluate")
            {
                GridViewRow row = (GridViewRow)((Button)e.CommandSource).NamingContainer;
                int employeeId = Convert.ToInt32(e.CommandArgument);

                TextBox txtRating = (TextBox)row.FindControl("txtRating");
                TextBox txtComments = (TextBox)row.FindControl("txtComments");
                TextBox txtSemester = (TextBox)row.FindControl("txtSemester");

                // Validate rating
                if (!int.TryParse(txtRating.Text.Trim(), out int rating) || rating < 1 || rating > 5)
                {
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                    lblMessage.Text = "Please enter a valid rating between 1 and 5.";
                    return;
                }

                // Validate semester
                string semester = txtSemester.Text.Trim();
                if (semester.Length != 3)
                {
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                    lblMessage.Text = "Semester must consist of exactly 3 characters (e.g., W23).";
                    return;
                }

                string comment = txtComments.Text.Trim();

                // Submit evaluation
                if (SubmitEvaluation(employeeId, rating, comment, semester))
                {
                    // Refresh GridView to remove submitted employee
                    LoadDepartmentEmployees(semester);
                }
            }
        }

        private bool SubmitEvaluation(int employeeId, int rating, string comment, string semester)
        {
            string connString = ConfigurationManager.ConnectionStrings["MyDatabaseConnection"].ConnectionString;

            try
            {
                using (SqlConnection conn = new SqlConnection(connString))
                using (SqlCommand cmd = new SqlCommand("Dean_andHR_Evaluation", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@employee_ID", employeeId);
                    cmd.Parameters.AddWithValue("@rating", rating);
                    cmd.Parameters.AddWithValue("@comment", comment);
                    cmd.Parameters.AddWithValue("@semester", semester);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }

                lblMessage.ForeColor = System.Drawing.Color.Green;
                lblMessage.Text = $"Evaluation submitted successfully for employee ID {employeeId}.";
                return true;
            }
            catch (Exception ex)
            {
                lblMessage.ForeColor = System.Drawing.Color.Red;
                lblMessage.Text = "Error submitting evaluation: " + ex.Message;
                return false;
            }
        }
    }
}
