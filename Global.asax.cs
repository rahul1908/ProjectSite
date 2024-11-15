using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.Http;
using System.Web.UI;

namespace ProjectSite
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            // Map the jQuery script required for Unobtrusive Validation
            ScriptManager.ScriptResourceMapping.AddDefinition(
                "jquery",
                new ScriptResourceDefinition
                {
                    Path = "~/Scripts/jquery-3.6.0.min.js",  // Adjust the path as per your project structure
            DebugPath = "~/Scripts/jquery-3.6.0.js"
                });

            ScriptManager.ScriptResourceMapping.AddDefinition(
    "bootstrap",
    new ScriptResourceDefinition
    {
        Path = "~/Scripts/bootstrap.min.js",  // Ensure this path ends in .js
        DebugPath = "~/Scripts/bootstrap.js"   // Ensure this path also ends in .js
    });
            // Code that runs on application startup
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);            
        }

        protected void Session_Start(object sender, EventArgs e)
        {
            Session.Add("user_id", 0);
            Session.Add("user_email", null);
            Session.Add("employee_id", null);
            Session.Add("project_id", 0);
            Session.Add("assignment_id", 0);
            Session.Add("manager_id", 0);
            Session.Add("MaxDisbursementClaimID", null);
            Session.Add("assignment_balance", 0);
            Session.Add("disbursement_id_to_update", null);
        }
    }
}