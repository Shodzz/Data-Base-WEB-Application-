using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.UI.WebControls;

namespace YourNamespace
{
    public partial class LastMonthPayroll : System.Web.UI.Page
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

        protected void btnShowPayroll_Click(object sender, EventArgs e)
        {
            string connString = ConfigurationManager.ConnectionStrings["MyDatabaseConnection"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connString))
            {
                try
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT * FROM dbo.Last_month_payroll(@employee_ID)", conn))
                    {
                        cmd.Parameters.Add("@employee_ID", SqlDbType.Int).Value = employeeID;

                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        gvPayroll.DataSource = dt;
                        gvPayroll.DataBind();

                        lblMessage.Text = dt.Rows.Count > 0
                            ? "Payroll records loaded."
                            : "No payroll records found for last month.";
                    }
                }
                catch (Exception ex)
                {
                    lblMessage.Text = "Database error: " + ex.Message;
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                }
            }
        }

        protected void gvPayroll_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView drv = e.Row.DataItem as DataRowView;
                if (drv != null)
                {
                    foreach (string colName in new[] { "payment_date", "from_date", "to_date" })
                    {
                        if (drv.DataView.Table.Columns.Contains(colName))
                        {
                            object val = drv[colName];
                            if (val != DBNull.Value)
                            {
                                DateTime d = Convert.ToDateTime(val);
                                e.Row.Cells[drv.DataView.Table.Columns[colName].Ordinal].Text = d.ToString("yyyy-MM-dd");
                                e.Row.Cells[drv.DataView.Table.Columns[colName].Ordinal].CssClass = "date-col";
                            }
                        }
                    }
                }
            }
            else if (e.Row.RowType == DataControlRowType.Header)
            {
                DataTable dt = gvPayroll.DataSource as DataTable;
                if (dt != null)
                {
                    foreach (string colName in new[] { "payment_date", "from_date", "to_date" })
                    {
                        if (dt.Columns.Contains(colName))
                        {
                            int colIndex = dt.Columns[colName].Ordinal;
                            e.Row.Cells[colIndex].CssClass = "date-col";
                        }
                    }
                }
            }
        }
    }
}
