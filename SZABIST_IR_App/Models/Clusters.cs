using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SZABIST_IR_App.Models
{
    public class Clusters
    {
        public string sUser_Id { get; set; }
        public byte tCampus_Id { get; set; }
        public string sUserProfile_FirstName { get; set; }
        public string sUserProfile_MiddleName { get; set; }
        public string sUserProfile_LastName { get; set; }
        public string Full_Name { get; set; }
        public Nullable<bool> bSystemRoleMember_Active { get; set; }
        public decimal iSystemRole_Id { get; set; }
    }
}