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
    public partial class HRDashboard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["HR_ID"] == null)
            {
                Response.Redirect("Login.aspx");
                return;
            }

            if (!IsPostBack)
            {
                int hrID = Convert.ToInt32(Session["HR_ID"]);
                string connStr = ConfigurationManager.ConnectionStrings["MyDatabaseConnection"].ConnectionString;

                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    SqlCommand cmd = new SqlCommand(@"
                        SELECT employee_id, first_name, last_name, dept_name
                        FROM Employee
                        WHERE employee_id = @HR_ID", conn);

                    cmd.Parameters.AddWithValue("@HR_ID", hrID);
                    conn.Open();

                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        lblHRID.Text = reader["employee_id"].ToString();
                        lblHRName.Text = reader["first_name"].ToString() + " " + reader["last_name"].ToString();
                        lblHRDept.Text = reader["dept_name"].ToString();
                    }
                }
            }
        }
    }
}