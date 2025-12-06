using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HR
{
    public partial class FetchWinterPerformance : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadWinterPerformance();
            }
        }

        private void LoadWinterPerformance()
        {
            string connStr = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["MyDatabaseConnection"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                // Fetch all performance records where semester = 'Win'
                SqlCommand cmd = new SqlCommand(@"
                    SELECT p.performance_ID, p.rating, p.comments, p.semester,
                           e.first_name + ' ' + e.last_name AS employee_name
                    FROM Performance p
                    JOIN Employee e ON p.emp_ID = e.employee_id
                    WHERE p.semester like 'W%'", conn);

                conn.Open();
                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    gvWinterPerformance.DataSource = rdr;
                    gvWinterPerformance.DataBind();
                }
            }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("Courses.aspx");
        }
    }
}