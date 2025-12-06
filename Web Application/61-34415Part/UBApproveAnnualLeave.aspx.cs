using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace _61_34415Part
{
    public partial class UBApproveAnnualLeave : System.Web.UI.Page
    {
        private int LoggedInEmployeeID
        {
            get
            {
                if (Session["EmployeeID"] != null && int.TryParse(Session["EmployeeID"].ToString(), out int id))
                    return id;
                throw new InvalidOperationException("User not logged in.");

                //return 15; // placeholder for testing
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                LoadPendingAnnualRequests();
        }

        private void LoadPendingAnnualRequests()
        {
            string connString = ConfigurationManager.ConnectionStrings["MyDatabaseConnection"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connString))
            using (SqlCommand cmd = new SqlCommand(@"
                SELECT L.request_ID,
                       E.first_name + ' ' + E.last_name AS employee_name,
                       L.start_date,
                       L.end_date,
                       R.first_name + ' ' + R.last_name AS replacement_name
                FROM Leave L
                INNER JOIN Annual_Leave AL ON L.request_ID = AL.request_ID
                INNER JOIN Employee E ON AL.emp_ID = E.employee_id
                INNER JOIN Employee R ON AL.replacement_emp = R.employee_id
                INNER JOIN Employee_Approve_Leave EA ON L.request_ID = EA.leave_ID
                WHERE EA.Emp1_ID = @empID AND EA.status = 'Pending'
                ORDER BY L.date_of_request DESC", conn))
            {
                cmd.Parameters.AddWithValue("@empID", LoggedInEmployeeID);
                conn.Open();

                DataTable dt = new DataTable();
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    da.Fill(dt);

                gvPendingAnnual.DataSource = dt;
                gvPendingAnnual.DataBind();

                if (dt.Rows.Count == 0)
                {
                    lblMessageAnnual.ForeColor = System.Drawing.Color.DarkBlue;
                    lblMessageAnnual.Text = "No pending annual leave requests assigned to you.";
                }
                else
                {
                    lblMessageAnnual.Text = "";
                }
            }
        }

        protected void gvPendingAnnual_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Process")
            {
                if (int.TryParse(Convert.ToString(e.CommandArgument), out int requestId))
                {
                    ProcessAnnualRequest(requestId);
                }
                else
                {
                    lblMessageAnnual.ForeColor = System.Drawing.Color.Red;
                    lblMessageAnnual.Text = "Could not determine request ID for processing.";
                }
            }
        }

        private void ProcessAnnualRequest(int requestId)
        {
            string connString = ConfigurationManager.ConnectionStrings["MyDatabaseConnection"].ConnectionString;

            try
            {
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    conn.Open(); // ✅ Open first

                    int replacementId = GetReplacementID(requestId, conn);

                    using (SqlCommand cmd = new SqlCommand("Upperboard_approve_annual", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@request_ID", requestId);
                        cmd.Parameters.AddWithValue("@Upperboard_ID", LoggedInEmployeeID);
                        cmd.Parameters.AddWithValue("@replacement_ID", replacementId);

                        cmd.ExecuteNonQuery();
                    }
                }

                lblMessageAnnual.ForeColor = System.Drawing.Color.Green;
                lblMessageAnnual.Text = $"Request {requestId} processed successfully.";

                LoadPendingAnnualRequests(); // refresh GridView
            }
            catch (Exception ex)
            {
                lblMessageAnnual.ForeColor = System.Drawing.Color.Red;
                lblMessageAnnual.Text = "Error processing request: " + ex.Message;
            }
        }

        private int GetReplacementID(int requestId, SqlConnection conn)
        {
            using (SqlCommand cmd = new SqlCommand("SELECT replacement_emp FROM Annual_Leave WHERE request_ID = @reqID", conn))
            {
                cmd.Parameters.AddWithValue("@reqID", requestId);
                if (conn.State != ConnectionState.Open) conn.Open();
                object val = cmd.ExecuteScalar();
                return (val != null) ? Convert.ToInt32(val) : 0;
            }
        }
    }
}
