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
    
    public partial class tblQuestionGrading
    {
        public int tGID { get; set; }
        public string GradeDescription { get; set; }
        public Nullable<int> complianceLevel { get; set; }
        public Nullable<int> GradePoint { get; set; }
        public Nullable<System.DateTime> CreationDate { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
    }
}
