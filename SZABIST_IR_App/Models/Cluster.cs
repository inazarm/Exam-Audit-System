using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SZABIST_IR_App.Models
{
    public partial class Cluster
    {
        public byte tCluster_ID { get; set; }
        public string ClusterTitle { get; set; }
        public string ClusterHeadID { get; set; }
        public string HeadName { get; set; }
        public Nullable<System.DateTime> ClusterEff_Date { get; set; }
        public string ClusterDescription { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }

        public DateTime CreatedDate { get; set; }
        public Nullable<bool> Status { get; set; }
    }
}