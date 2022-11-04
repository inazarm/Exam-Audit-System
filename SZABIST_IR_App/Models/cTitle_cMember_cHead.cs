using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SZABIST_IR_App.Models
{
    public class cTitle_cMember_cHead
    {
        public tblClusterInfo cTitle { get; set; }
        public tblClusterMember cMember { get; set; }
        public tblClusterHead cHead { get; set; }
    }
}