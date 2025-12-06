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
    public partial class Login : System.Web.UI.Page
    {
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string enteredID = txtUserID.Text.Trim();
            string enteredPassword = txtPassword.Text.Trim();

            // 1. Check Admin (hard-coded)
            if (enteredID == "admin" && enteredPassword == "admin123")
            {
                Session["Role"] = "Admin";
                Response.Redirect("courses.aspx");
                return;
            }

            // 2. Validate that enteredID is numeric before calling SQL functions
            if (!int.TryParse(enteredID, out int numericID))
            {
                lblMessage.Text = "❌ Invalid ID format. Employee ID must be numeric.";
                lblMessage.Visible = true;
                return;
            }

            string connStr = ConfigurationManager.ConnectionStrings["MyDatabaseConnection"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();

                // 3. Check HR using HRLoginValidation
                SqlCommand cmdHR = new SqlCommand("SELECT dbo.HRLoginValidation(@id,@pwd)", conn);
                cmdHR.Parameters.AddWithValue("@id", numericID);
                cmdHR.Parameters.AddWithValue("@pwd", enteredPassword);

                bool hrValid = Convert.ToBoolean(cmdHR.ExecuteScalar());
                if (hrValid)
                {
                    Session["Role"] = "HR";
                    Session["HR_ID"] = numericID;
                    Response.Redirect("HRDashboard.aspx");
                    return;
                }

                // 4. Check Employee using EmployeeLoginValidation
                SqlCommand cmdEmp = new SqlCommand("SELECT dbo.EmployeeLoginValidation(@id,@pwd)", conn);
                cmdEmp.Parameters.AddWithValue("@id", numericID);
                cmdEmp.Parameters.AddWithValue("@pwd", enteredPassword);

                bool empValid = Convert.ToBoolean(cmdEmp.ExecuteScalar());
                if (empValid)
                {
                    Session["Role"] = "Employee";
                    Session["EmployeeID"] = numericID;
                    Response.Redirect("DASH BOARD.aspx");
                    return;
                }
            }

            // 5. If none matched
            lblMessage.Text = "❌ Invalid ID or Password.";
            lblMessage.Visible = true;
        }
    }
}