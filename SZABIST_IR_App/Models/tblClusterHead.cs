//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SZABIST_IR_App.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class tblClusterHead
    {
        public int tCluster_Head_Id { get; set; }
        public Nullable<int> tCluster_ID { get; set; }
        public Nullable<int> tMemberID { get; set; }
        public string ClusterHeadID { get; set; }
        public string ClusterHeadName { get; set; }
        public Nullable<System.DateTime> WEF_From { get; set; }
        public Nullable<System.DateTime> WEF_To { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<byte> CampusID { get; set; }
        public Nullable<bool> Status { get; set; }
    
        public virtual tblClusterInfo tblClusterInfo { get; set; }
    }
}
