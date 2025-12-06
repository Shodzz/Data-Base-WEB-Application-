using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HR
{
    public partial class UpdateAttendance : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Optional: you can add Page_Load logic here if needed
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            int empID;
            TimeSpan checkIn, checkOut;

            // Validate Employee ID
            if (!int.TryParse(txtEmployeeID.Text.Trim(), out empID))
            {
                lblMessage.ForeColor = System.Drawing.Color.Red;
                lblMessage.Text = "❌ Invalid Employee ID!";
                return;
            }

            // Validate Check-In and Check-Out times
            if (!TimeSpan.TryParse(txtCheckIn.Text.Trim(), out checkIn) ||
                !TimeSpan.TryParse(txtCheckOut.Text.Trim(), out checkOut))
            {
                lblMessage.ForeColor = System.Drawing.Color.Red;
                lblMessage.Text = "❌ Invalid time format! Please use HH:mm (e.g., 09:30).";
                return;
            }

            string connStr = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["MyDatabaseConnection"].ToString();

            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    SqlCommand cmd = new SqlCommand("Update_Attendance", conn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    // Parameters
                    cmd.Parameters.AddWithValue("@employee_ID", empID);
                    cmd.Parameters.AddWithValue("@check_in_time", checkIn);
                    cmd.Parameters.AddWithValue("@check_out_time", checkOut);

                    conn.Open();
                    cmd.ExecuteNonQuery();

                    lblMessage.ForeColor = System.Drawing.Color.LimeGreen;
                    lblMessage.Text = "✅ Attendance updated successfully!";
                }
            }
            catch (Exception ex)
            {
                lblMessage.ForeColor = System.Drawing.Color.Red;
                lblMessage.Text = "❌ Error updating attendance: " + ex.Message;
            }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("Courses.aspx");
        }
    }
}