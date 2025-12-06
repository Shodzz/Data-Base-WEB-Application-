using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HR
{
    public partial class ReplaceEmployee : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnReplaceEmployee_Click(object sender, EventArgs e)
        {
            int emp1, emp2;
            DateTime fromDate, toDate;

            if (!int.TryParse(txtEmp1.Text.Trim(), out emp1) ||
                !int.TryParse(txtEmp2.Text.Trim(), out emp2) ||
                !DateTime.TryParse(txtFromDate.Text.Trim(), out fromDate) ||
                !DateTime.TryParse(txtToDate.Text.Trim(), out toDate))
            {
                lblMessage.ForeColor = System.Drawing.Color.Red;
                lblMessage.Text = "❌ Invalid input. Please check Employee IDs and dates.";
                return;
            }

            string connStr = System.Web.Configuration.WebConfigurationManager
                                .ConnectionStrings["MyDatabaseConnection"].ConnectionString;

            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    SqlCommand cmd = new SqlCommand("dbo.Replace_employee", conn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Emp1_ID", emp1);
                    cmd.Parameters.AddWithValue("@Emp2_ID", emp2);
                    cmd.Parameters.AddWithValue("@from_date", fromDate);
                    cmd.Parameters.AddWithValue("@to_date", toDate);

                    conn.Open();
                    cmd.ExecuteNonQuery();

                    lblMessage.ForeColor = System.Drawing.Color.LimeGreen;
                    lblMessage.Text = $"✅ Employee {emp1} replaced by Employee {emp2} from {fromDate:yyyy-MM-dd} to {toDate:yyyy-MM-dd}.";
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