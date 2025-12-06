using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HR
{
    public partial class DeductUnpaidLeave : System.Web.UI.Page
    {
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            int empID;
            if (!int.TryParse(txtEmpID.Text, out empID))
            {
                lblMessage.CssClass = "error-message";
                lblMessage.Text = "❌ Please enter a valid Employee ID.";
                lblMessage.Visible = true;
                return;
            }

            string connStr = ConfigurationManager.ConnectionStrings["MyDatabaseConnection"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();

                // 🔹 Step 1: Check if employee exists
                using (SqlCommand checkCmd = new SqlCommand("SELECT COUNT(*) FROM Employee WHERE employee_id = @empID", conn))
                {
                    checkCmd.Parameters.AddWithValue("@empID", empID);
                    int exists = (int)checkCmd.ExecuteScalar();

                    if (exists == 0)
                    {
                        lblMessage.CssClass = "error-message";
                        lblMessage.Text = $"❌ Employee ID {empID} not found in the database.";
                        lblMessage.Visible = true;
                        return; // stop here
                    }
                }

                // 🔹 Step 2: Run deduction procedure
                using (SqlCommand cmd = new SqlCommand("Deduction_unpaid", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@employee_ID", empID);

                    try
                    {
                        cmd.ExecuteNonQuery();
                        lblMessage.CssClass = "success-message";
                        lblMessage.Text = "✅ Deduction for unpaid leave applied successfully.";
                    }
                    catch (SqlException ex)
                    {
                        lblMessage.CssClass = "error-message";
                        lblMessage.Text = "❌ Error: " + ex.Message;
                    }
                    lblMessage.Visible = true;
                }
            }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("HRDashboard.aspx"); // send user back to HR Dashboard
        }
    }
}