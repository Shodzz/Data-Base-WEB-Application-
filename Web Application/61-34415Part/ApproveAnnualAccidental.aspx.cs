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
    public partial class ApproveAnnualAccidental : System.Web.UI.Page
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
                LoadPendingLeaves();
            }
        }

        private void LoadPendingLeaves()
        {
            int hrID = Convert.ToInt32(Session["HR_ID"]);
            string connStr = ConfigurationManager.ConnectionStrings["MyDatabaseConnection"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connStr))
            using (SqlCommand cmd = new SqlCommand(@"
                SELECT L.request_ID,
                       COALESCE(AL.emp_ID, ACC.emp_ID) AS emp_ID,
                       L.start_date, L.end_date, L.num_days,
                       CASE 
                           WHEN AL.request_ID IS NOT NULL THEN 'Annual'
                           WHEN ACC.request_ID IS NOT NULL THEN 'Accidental'
                       END AS leave_type
                FROM Leave L
                LEFT JOIN Annual_Leave AL ON L.request_ID = AL.request_ID
                LEFT JOIN Accidental_Leave ACC ON L.request_ID = ACC.request_ID
                JOIN Employee_Approve_Leave EAL ON L.request_ID = EAL.leave_ID
                WHERE EAL.status = 'Pending'
                  AND (AL.request_ID IS NOT NULL OR ACC.request_ID IS NOT NULL)
                  AND EAL.Emp1_ID = @HR_ID", conn))
            {
                cmd.Parameters.AddWithValue("@HR_ID", hrID);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                gvLeaves.DataSource = dt;
                gvLeaves.DataBind();
            }
        }

        protected void gvLeaves_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Process")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                int requestID = Convert.ToInt32(gvLeaves.DataKeys[index].Value);
                int hrID = Convert.ToInt32(Session["HR_ID"]);

                string connStr = ConfigurationManager.ConnectionStrings["MyDatabaseConnection"].ConnectionString;
                using (SqlConnection conn = new SqlConnection(connStr))
                using (SqlCommand cmd = new SqlCommand("HR_approval_an_acc", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@request_ID", requestID);
                    cmd.Parameters.AddWithValue("@HR_ID", hrID);

                    conn.Open();
                    try
                    {
                        cmd.ExecuteNonQuery();
                        lblMessage.CssClass = "success-message";
                        lblMessage.Text = "✅ Leave processed successfully.";
                    }
                    catch (SqlException)
                    {
                        lblMessage.CssClass = "error-message";
                        lblMessage.Text = "❌ Error processing leave request.";
                    }
                    lblMessage.Visible = true;
                }

                LoadPendingLeaves(); // refresh grid
            }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("HRDashboard.aspx");
        }
    }
}