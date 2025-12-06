using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.UI.WebControls;

namespace YourNamespace
{
    public partial class MyDeduction : System.Web.UI.Page
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

        protected void btnLoadDeduction_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtMonth.Text.Trim(), out int month) || month < 1 || month > 12)
            {
                lblMessage.Text = "Please enter a valid month number (1-12).";
                lblMessage.ForeColor = System.Drawing.Color.Red;
                return;
            }

            string connString = ConfigurationManager.ConnectionStrings["MyDatabaseConnection"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connString))
            {
                try
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT * FROM dbo.Deductions_Attendance(@employee_ID, @month)", conn))
                    {
                        cmd.Parameters.Add("@employee_ID", SqlDbType.Int).Value = employeeID;
                        cmd.Parameters.Add("@month", SqlDbType.Int).Value = month;

                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        gvDeduction.DataSource = dt;
                        gvDeduction.DataBind();

                        lblMessage.Text = dt.Rows.Count > 0
                            ? "Deduction records loaded."
                            : "No deduction records found for this month.";
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

        protected void gvDeduction_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView drv = e.Row.DataItem as DataRowView;
                if (drv != null && drv.DataView.Table.Columns.Contains("date"))
                {
                    object val = drv["date"];
                    if (val != DBNull.Value)
                    {
                        DateTime d = Convert.ToDateTime(val);
                        e.Row.Cells[drv.DataView.Table.Columns["date"].Ordinal].Text = d.ToString("yyyy-MM-dd");
                        e.Row.Cells[drv.DataView.Table.Columns["date"].Ordinal].CssClass = "date-col";
                    }
                }
            }
            else if (e.Row.RowType == DataControlRowType.Header)
            {
                DataTable dt = gvDeduction.DataSource as DataTable;
                if (dt != null && dt.Columns.Contains("date"))
                {
                    int colIndex = dt.Columns["date"].Ordinal;
                    e.Row.Cells[colIndex].CssClass = "date-col";
                }
            }
        }
    }
}
