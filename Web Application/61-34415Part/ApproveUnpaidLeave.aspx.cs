using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HR
{
    public partial class ApproveUnpaidLeave : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadPendingLeaves();
            }
        }

        private void LoadPendingLeaves()
        {
            int hrID = Convert.ToInt32(Session["HR_ID"]);
            string connStr = ConfigurationManager.ConnectionStrings["MyDatabaseConnection"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand(@"
                SELECT L.request_ID,
                       EAL.Emp1_ID AS hr_ID,
                       UL.Emp_ID AS emp_ID,
                       L.start_date, L.end_date, L.num_days,
                       E.annual_balance,
                       E.accidental_balance
                FROM Employee_Approve_Leave EAL
                JOIN Leave L ON L.request_ID = EAL.leave_ID
                JOIN Unpaid_Leave UL ON L.request_ID = UL.request_ID
                JOIN Employee E ON E.employee_id = UL.Emp_ID
                WHERE EAL.status = 'Pending'
                  AND EAL.Emp1_ID = @HR_ID", conn);

                cmd.Parameters.AddWithValue("@HR_ID", hrID);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                gvLeaves.DataSource = dt;
                gvLeaves.DataBind();
            }
        }

        protected void gvLeaves_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Process")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = gvLeaves.Rows[index];
                int requestID = Convert.ToInt32(row.Cells[0].Text);
                int hrID = Convert.ToInt32(Session["HR_ID"]);

                string connStr = ConfigurationManager.ConnectionStrings["MyDatabaseConnection"].ConnectionString;
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    SqlCommand cmd = new SqlCommand("HR_approval_Unpaid", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@request_ID", requestID);
                    cmd.Parameters.AddWithValue("@HR_ID", hrID);

                    conn.Open();
                    try
                    {
                        cmd.ExecuteNonQuery();
                        lblMessage.Text = "✅ Leave processed successfully.";
                    }
                    catch (SqlException ex)
                    {
                        lblMessage.Text = "❌ Error: " + ex.Message;
                    }
                    lblMessage.Visible = true;
                    LoadPendingLeaves(); // refresh grid
                }
            }
        }
        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("HRDashboard.aspx"); // send user back to HR Dashboard
        }
    }
}