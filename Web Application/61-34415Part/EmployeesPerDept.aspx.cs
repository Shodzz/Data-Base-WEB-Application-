using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HR
{
    public partial class EmployeesPerDept : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadEmployeesPerDept();
            }
        }

        private void LoadEmployeesPerDept()
        {
            string connStr = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["MyDatabaseConnection"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM NoEmployeeDept", conn);
                conn.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                gvEmployeesPerDept.DataSource = rdr; // Updated to match .aspx
                gvEmployeesPerDept.DataBind();

                rdr.Close();
            }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("Courses.aspx");
        }
    }
}