using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI;

namespace ProjectSite.Restricted
{
    public partial class Manager_Reports : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Initially hide the report iframe until a report is selected
                reportContainer.Style["display"] = "none";
            }
        }

        protected void ddlReports_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedReportUrl = ddlReports.SelectedValue;

            if (!string.IsNullOrEmpty(selectedReportUrl))
            {
                // Set the iframe's src to the selected report's URL
                reportFrame.Attributes["src"] = selectedReportUrl;

                // Show the report iframe container
                reportContainer.Style["display"] = "block";
            }
            else
            {
                // Hide the report iframe if no valid selection is made
                reportContainer.Style["display"] = "none";
            }
        }
    }
}