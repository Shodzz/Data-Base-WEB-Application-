using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HR
{
    public partial class RemoveUnattendedDayoff : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnRemoveUnattendedDayoff_Click(object sender, EventArgs e)
        {
            int empID;
            if (!int.TryParse(txtEmpID.Text.Trim(), out empID))
            {
                lblMessage.ForeColor = System.Drawing.Color.Red;
                lblMessage.Text = "❌ Invalid Employee ID.";
                return;
            }

            string connStr = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["MyDatabaseConnection"].ConnectionString;

            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    SqlCommand cmd = new SqlCommand("dbo.Remove_DayOff", conn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@employee_ID", empID);

                    conn.Open();
                    int affected = cmd.ExecuteNonQuery();

                    lblMessage.ForeColor = System.Drawing.Color.LimeGreen;
                    lblMessage.Text = $"✅ {affected} unattended dayoff records removed for Employee ID {empID}.";
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