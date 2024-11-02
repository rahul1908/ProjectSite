using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProjectSite.Restricted
{
    public partial class UpdateDisbursements : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void txtMileage_TextChanged(object sender, EventArgs e)
        {
            // Example: Calculate the travel total based on mileage (assuming a fixed rate per mile).
            double mileage;
            if (double.TryParse(txtMileage.Text, out mileage))
            {
                double ratePerMile = 0.5; // Example rate, adjust as needed
                double travelTotal = mileage * ratePerMile;
                txtTravelTotal.Text = travelTotal.ToString("F2");
            }
            else
            {
                txtTravelTotal.Text = "0.00";
            }
            

        }

        protected void btnUpdateDisbursement_Click(object sender, EventArgs e)
        {
            //code to go
        }
    }
}