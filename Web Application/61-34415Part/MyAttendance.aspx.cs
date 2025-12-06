using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.UI.WebControls;

namespace YourNamespace
{
    public partial class MyAttendance : System.Web.UI.Page
    {
        private int employeeID;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["EmployeeID"] != null)
            {
                employeeID = (int)Session["EmployeeID"];
            }
            else
            {
                lblMessage.Text = "No Employee ID provided. Please log in again.";
                lblMessage.ForeColor = System.Drawing.Color.Red;
            }
        }

        protected void btnShowAttendance_Click(object sender, EventArgs e)
        {
            string connString = ConfigurationManager.ConnectionStrings["MyDatabaseConnection"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connString))
            {
                try
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT * FROM dbo.MyAttendance(@employee_ID)", conn))
                    {
                        cmd.Parameters.Add("@employee_ID", SqlDbType.Int).Value = employeeID;

                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        gvAttendance.DataSource = dt;
                        gvAttendance.DataBind();

                        lblMessage.Text = dt.Rows.Count > 0
                            ? "Attendance records loaded."
                            : "No attendance records found for this month.";
                    }
                }
                catch (Exception ex)
                {
                    lblMessage.Text = "Database error: " + ex.Message;
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                }
            }
        }

        protected void gvAttendance_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView drv = e.Row.DataItem as DataRowView;
                if (drv != null && drv.DataView.Table.Columns.Contains("date"))
                {
                    object val = drv["date"];
                    string formatted = string.Empty;

                    if (val != DBNull.Value)
                    {
                        DateTime d = Convert.ToDateTime(val);
                        formatted = d.ToString("yyyy-MM-dd"); // ✅ date-only
                    }

                    int colIndex = drv.DataView.Table.Columns["date"].Ordinal;
                    e.Row.Cells[colIndex].Text = formatted;

                    // Apply CSS class to widen the column
                    e.Row.Cells[colIndex].CssClass = "date-col";
                }
            }
            else if (e.Row.RowType == DataControlRowType.Header)
            {
                // Apply CSS class to header cell for date column
                DataTable dt = ((gvAttendance.DataSource as DataTable));
                if (dt != null && dt.Columns.Contains("date"))
                {
                    int colIndex = dt.Columns["date"].Ordinal;
                    e.Row.Cells[colIndex].CssClass = "date-col";
                }
            }
        }
    }
}
