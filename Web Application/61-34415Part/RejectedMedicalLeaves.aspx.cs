using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HR
{
    public partial class RejectedMedicalLeaves : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadRejectedMedicalLeaves();
            }
        }

        private void LoadRejectedMedicalLeaves()
        {
            // Always use MyDatabaseConnection
            string connStr = System.Web.Configuration.WebConfigurationManager
                                .ConnectionStrings["MyDatabaseConnection"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM allRejectedMedicals", conn);
                conn.Open();

                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    gvRejectedMedical.DataSource = rdr;
                    gvRejectedMedical.DataBind();
                }
            }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("Courses.aspx");
        }
    }
}