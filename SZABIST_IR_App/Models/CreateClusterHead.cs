using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SZABIST_IR_App.Models
{
    public class CreateClusterHead
    {
        public int tCluster_ID { get; set; }
        public int tMemberID { get; set; }
        public string ClusterTitle { get; set; }
        public int tCluster_Head_Id { get; set; }
        public string ClusterHeadID { get; set; }
        public string ClusterHeadName { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public Nullable<System.DateTime> WEF_From { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public Nullable<System.DateTime> WEF_To { get; set; }

        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public Nullable<System.DateTime> CreatedDate { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<byte> CampusID { get; set; }
        public Nullable<bool> Status { get; set; }
    }
}