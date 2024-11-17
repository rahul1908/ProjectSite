using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

namespace ProjectSite.Restricted
{
    public partial class ClientReports : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Set the Power BI report URL to the iframe on page load
            var reportFrame = (System.Web.UI.HtmlControls.HtmlIframe)FindControl("reportFrame");

            if (!IsPostBack)
            {
               
            }

            if (reportFrame != null)
            {
                // Power BI report URL (use your actual published URL here)
                string reportUrl = "https://app.powerbi.com/view?r=eyJrIjoiYzY2N2EzM2ItOGE3Mi00NTMxLWE0N2MtOTM1NmExYjYyZGFjIiwidCI6IjIyNjgyN2Q2LWE5ZDAtNDcwZC04YzE1LWIxNDZiMDE5MmQ1MSIsImMiOjh9";

                // Set the iframe's src attribute to the Power BI report URL
                reportFrame.Attributes["src"] = reportUrl;
            }
        }
    }
}