using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HR
{
    public partial class FetchYesterdayAttendance : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadYesterdayAttendance();
            }
        }

        private void LoadYesterdayAttendance()
        {
            string connStr = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["MyDatabaseConnection"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                // Yesterday’s date
                DateTime yesterday = DateTime.Today.AddDays(-1);

                SqlCommand cmd = new SqlCommand("SELECT * FROM Attendance WHERE CAST(date AS DATE) = @yesterday", conn);

                cmd.Parameters.AddWithValue("@yesterday", yesterday);

                conn.Open();
                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    gvYesterdayAttendance.DataSource = rdr;
                    gvYesterdayAttendance.DataBind();
                }
            }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("Courses.aspx");
        }
    }
}