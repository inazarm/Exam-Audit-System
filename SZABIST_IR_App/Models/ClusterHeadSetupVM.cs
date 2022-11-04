using System;
using System.ComponentModel.DataAnnotations;

namespace SZABIST_IR_App.Models
{
    public class ClusterHeadSetupVM
    {

        [Required(ErrorMessage = "Please Enter Title"), MaxLength(100)]
        public string ClusterTitle { get; set; }

        [Required(ErrorMessage = "Please Select Head Name"), MaxLength(30)]
        public string ClusterHead { get; set; }

        [DataType(DataType.Date)]
        public Nullable<System.DateTime> ClusterEff_Date { get; set; }
        public string ClusterDescription { get; set; }

        public string ClusterMemberID { get; set; }
        public string ClusterMemberName { get; set; }
        public string ClusterHeadName { get; set; }
        public Nullable<System.DateTime> WEF_From { get; set; }
        public Nullable<System.DateTime> WEF_To { get; set; }
    }
}