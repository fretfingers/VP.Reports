//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace VP.Reports
{
    using System;
    using System.Collections.Generic;
    
    public partial class CostCenter
    {
        public string Company { get; set; }
        public string Branch { get; set; }
        public string CostCenterID { get; set; }
        public string Description { get; set; }
        public string LockedBy { get; set; }
        public Nullable<System.DateTime> LockTS { get; set; }
        public string GLAccountNumber { get; set; }
        public Nullable<double> ChargeOutRate { get; set; }
    }
}
