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
    public partial class AddHoliday : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnAddHoliday_Click(object sender, EventArgs e)
        {
            string holidayName = txtHolidayName.Text.Trim();
            string fromDate = txtFromDate.Text.Trim();
            string toDate = txtToDate.Text.Trim();

            if (string.IsNullOrEmpty(holidayName) || string.IsNullOrEmpty(fromDate) || string.IsNullOrEmpty(toDate))
            {
                lblMessage.ForeColor = System.Drawing.Color.Red;
                lblMessage.Text = "❌ Please fill in all fields.";
                return;
            }

            string connStr = ConfigurationManager.ConnectionStrings["MyDatabaseConnection"].ConnectionString;

            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    SqlCommand cmd = new SqlCommand("Add_Holiday", conn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    // Pass parameters to stored procedure
                    cmd.Parameters.AddWithValue("@holiday_name", holidayName);
                    cmd.Parameters.AddWithValue("@from_date", DateTime.Parse(fromDate));
                    cmd.Parameters.AddWithValue("@to_date", DateTime.Parse(toDate));

                    conn.Open();
                    cmd.ExecuteNonQuery();

                    lblMessage.ForeColor = System.Drawing.Color.LimeGreen;
                    lblMessage.Text = "✅ Holiday created successfully!";
                }
            }
            catch (Exception ex)
            {
                lblMessage.ForeColor = System.Drawing.Color.Red;
                lblMessage.Text = "❌ Error: " + ex.Message;
            }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("Courses.aspx");
        }
    }
}