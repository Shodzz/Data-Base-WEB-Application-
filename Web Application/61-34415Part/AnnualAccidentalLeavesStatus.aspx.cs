using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.UI.WebControls;

namespace YourNamespace
{
    public partial class AnnualAccidentalLeaveStatus : System.Web.UI.Page
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

        protected void btnLoadStatus_Click(object sender, EventArgs e)
        {
            string connString = ConfigurationManager.ConnectionStrings["MyDatabaseConnection"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connString))
            {
                try
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT * FROM dbo.status_leaves(@employee_ID)", conn))
                    {
                        cmd.Parameters.Add("@employee_ID", SqlDbType.Int).Value = employeeID;

                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        gvLeaveStatus.DataSource = dt;
                        gvLeaveStatus.DataBind();

                        lblMessage.Text = dt.Rows.Count > 0
                            ? "Leave status records loaded."
                            : "No leave records found for this month.";
                        lblMessage.ForeColor = dt.Rows.Count > 0
                            ? System.Drawing.Color.Green
                            : System.Drawing.Color.Red;
                    }
                }
                catch (Exception ex)
                {
                    lblMessage.Text = "Database error: " + ex.Message;
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                }
            }
        }

        protected void gvLeaveStatus_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView drv = e.Row.DataItem as DataRowView;
                if (drv != null && drv.DataView.Table.Columns.Contains("date_of_request"))
                {
                    object val = drv["date_of_request"];
                    if (val != DBNull.Value)
                    {
                        DateTime d = Convert.ToDateTime(val);
                        e.Row.Cells[drv.DataView.Table.Columns["date_of_request"].Ordinal].Text = d.ToString("yyyy-MM-dd");
                        e.Row.Cells[drv.DataView.Table.Columns["date_of_request"].Ordinal].CssClass = "date-col";
                    }
                }
            }
            else if (e.Row.RowType == DataControlRowType.Header)
            {
                DataTable dt = gvLeaveStatus.DataSource as DataTable;
                if (dt != null && dt.Columns.Contains("date_of_request"))
                {
                    int colIndex = dt.Columns["date_of_request"].Ordinal;
                    e.Row.Cells[colIndex].CssClass = "date-col";
                }
            }
        }
    }
}
