using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HR
{
    public partial class courses : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Nothing needed here for now
        }

        // Existing buttons
        protected void ViewEmployees_Click(object sender, EventArgs e) => Response.Redirect("ViewEmployees.aspx");
        protected void EmployeesPerDept_Click(object sender, EventArgs e) => Response.Redirect("EmployeesPerDept.aspx");
        protected void RejectedMedical_Click(object sender, EventArgs e) => Response.Redirect("RejectedMedicalLeaves.aspx");
        protected void RemoveDeductions_Click(object sender, EventArgs e) => Response.Redirect("RemoveDeductions.aspx");
        protected void UpdateAttendance_Click(object sender, EventArgs e) => Response.Redirect("UpdateAttendance.aspx");
        protected void AddHoliday_Click(object sender, EventArgs e) => Response.Redirect("AddHoliday.aspx");
        protected void InitiateAttendance_Click(object sender, EventArgs e) => Response.Redirect("InitiateAttendance.aspx");

        // New admin operations
        protected void FetchYesterdayAttendance_Click(object sender, EventArgs e) => Response.Redirect("FetchYesterdayAttendance.aspx");
        protected void FetchWinterPerformance_Click(object sender, EventArgs e) => Response.Redirect("FetchWinterPerformance.aspx");
        protected void RemoveHolidayAttendance_Click(object sender, EventArgs e) => Response.Redirect("RemoveHolidayAttendance.aspx");
        protected void RemoveUnattendedDayoff_Click(object sender, EventArgs e) => Response.Redirect("RemoveUnattendedDayoff.aspx");
        protected void RemoveApprovedLeaves_Click(object sender, EventArgs e) => Response.Redirect("RemoveApprovedLeaves.aspx");
        protected void ReplaceEmployee_Click(object sender, EventArgs e) => Response.Redirect("ReplaceEmployee.aspx");
        protected void UpdateEmploymentStatus_Click(object sender, EventArgs e) => Response.Redirect("UpdateEmploymentStatus.aspx");
    }
}