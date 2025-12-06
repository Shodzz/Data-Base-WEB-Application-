using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace _61_34415Part
{
    public partial class UBApproveUnpaidLeave : System.Web.UI.Page
    {

        private int LoggedInEmployeeID
        {
            get
            {
                  if (Session["EmployeeID"] != null && int.TryParse(Session["EmployeeID"].ToString(), out int id))
                    return id;
                 
                 throw new InvalidOperationException("User not logged in.");
                
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                LoadPendingRequests();
        }

        private void LoadPendingRequests()
        {
            string connString = ConfigurationManager.ConnectionStrings["MyDatabaseConnection"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connString))
            using (SqlCommand cmd = new SqlCommand(@"
                SELECT L.request_ID, 
                       E.first_name + ' ' + E.last_name AS employee_name, 
                       L.start_date, 
                       L.end_date
                FROM Leave L
                INNER JOIN Unpaid_Leave UL ON L.request_ID = UL.request_ID
                INNER JOIN Employee E ON UL.Emp_ID = E.employee_id
                INNER JOIN Employee_Approve_Leave EA ON L.request_ID = EA.leave_ID
                WHERE EA.Emp1_ID = @empID AND UPPER(EA.status) = 'PENDING'
                ORDER BY L.date_of_request DESC", conn))
            {
                cmd.Parameters.AddWithValue("@empID", LoggedInEmployeeID);
                conn.Open();

                DataTable dt = new DataTable();
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    da.Fill(dt);

                gvPendingRequests.DataSource = dt;
                gvPendingRequests.DataBind();

                if (dt.Rows.Count == 0)
                {
                    lblMessage.ForeColor = System.Drawing.Color.DarkBlue;
                    lblMessage.Text = "No pending unpaid leave requests assigned to you.";
                }
                else
                {
                    lblMessage.Text = "";
                }
            }
        }

        protected void gvPendingRequests_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Process")
            {
                // GridView passes CommandArgument = request_ID
                if (int.TryParse(Convert.ToString(e.CommandArgument), out int requestId))
                {
                    ProcessRequest(requestId);
                }
                else
                {
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                    lblMessage.Text = "Could not determine request ID for processing.";
                }
            }
        }

        private void ProcessRequest(int requestId)
        {
            string connString = ConfigurationManager.ConnectionStrings["MyDatabaseConnection"].ConnectionString;

            try
            {
                using (SqlConnection conn = new SqlConnection(connString))
                using (SqlCommand cmd = new SqlCommand("Upperboard_approve_unpaids", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@request_ID", requestId);
                    cmd.Parameters.AddWithValue("@upperboard_ID", LoggedInEmployeeID);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }

                lblMessage.ForeColor = System.Drawing.Color.Green;
                lblMessage.Text = $"Request {requestId} processed successfully.";

                // reload pending requests to remove processed leave
                LoadPendingRequests();
            }
            catch (Exception ex)
            {
                lblMessage.ForeColor = System.Drawing.Color.Red;
                lblMessage.Text = "Error processing request: " + ex.Message;
            }
        }
    }
}
