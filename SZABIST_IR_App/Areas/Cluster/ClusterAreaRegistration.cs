using System.Web.Mvc;

namespace SZABIST_IR_App.Areas.Cluster
{
    public class ClusterAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Cluster";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Cluster_default",
                "Cluster/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
                new[] { "SZABIST_IR_App.Areas.Cluster.Controllers" } 
            );
        }
    }
}