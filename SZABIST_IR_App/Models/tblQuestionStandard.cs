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
    
    public partial class tblQuestionStandard
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tblQuestionStandard()
        {
            this.tblQuestionnaires = new HashSet<tblQuestionnaire>();
        }
    
        public int tSID { get; set; }
        public Nullable<int> StandardNo { get; set; }
        public string StandardDescription { get; set; }
        public Nullable<decimal> StandardPecentage { get; set; }
        public Nullable<int> DepartID { get; set; }
        public Nullable<System.DateTime> CreationDate { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblQuestionnaire> tblQuestionnaires { get; set; }
    }
}
