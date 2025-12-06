using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HR
{
    public partial class InitiateAttendance : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnInitiate_Click(object sender, EventArgs e)
        {
            // Always use MyDatabaseConnection
            string connStr = System.Web.Configuration.WebConfigurationManager
                                .ConnectionStrings["MyDatabaseConnection"].ConnectionString;

            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    SqlCommand cmd = new SqlCommand("Initiate_Attendance", conn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    conn.Open();
                    cmd.ExecuteNonQuery();

                    lblMessage.ForeColor = System.Drawing.Color.LimeGreen;
                    lblMessage.Text = "✅ Attendance records initiated for today!";
                }
            }
            catch (Exception ex)
            {
                lblMessage.ForeColor = System.Drawing.Color.Red;
                lblMessage.Text = "❌ Error initiating attendance: " + ex.Message;
            }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("Courses.aspx");
        }
    }
}