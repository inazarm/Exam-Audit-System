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
    
    public partial class vueExamList_Checked_NotAssessed
    {
        public int tAllocateID { get; set; }
        public Nullable<int> tCluster_Head_Id { get; set; }
        public Nullable<byte> tCampus_Id { get; set; }
        public string ClusterHeadID { get; set; }
        public string ClusterHeadName { get; set; }
        public string Year { get; set; }
        public string Semster { get; set; }
        public string Program { get; set; }
        public string Program_ShortDesc { get; set; }
        public string Program_LongDesc { get; set; }
        public string ExamTypeName { get; set; }
        public Nullable<bool> Status { get; set; }
        public Nullable<bool> isChecked { get; set; }
        public Nullable<bool> isCheckedByIR { get; set; }
        public Nullable<byte> tFaculty_Id { get; set; }
        public string Faculty_ShortDesc { get; set; }
        public Nullable<byte> ExamType { get; set; }
        public Nullable<bool> isCopyAvailable { get; set; }
        public string Campus { get; set; }
    }
}
