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
    
    public partial class EmployeeQualification
    {
        public string Company { get; set; }
        public string Branch { get; set; }
        public string EmployeeID { get; set; }
        public string QualificationTypeID { get; set; }
        public string Institution { get; set; }
        public string YearFrom { get; set; }
        public string YearTo { get; set; }
        public string Grade { get; set; }
        public string CourseTypeID { get; set; }
        public string CertificateNo { get; set; }
        public Nullable<int> YearofIssuance { get; set; }
        public Nullable<bool> Entered { get; set; }
        public string EnteredBy { get; set; }
        public Nullable<System.DateTime> EnteredDate { get; set; }
        public Nullable<bool> Posted { get; set; }
        public string PostedBy { get; set; }
        public Nullable<System.DateTime> PostedDate { get; set; }
        public string VerifiedBy { get; set; }
        public Nullable<bool> Verified { get; set; }
        public Nullable<System.DateTime> VerifiedDate { get; set; }
        public string LockedBy { get; set; }
        public Nullable<System.DateTime> LockTS { get; set; }
        public int QualificationID { get; set; }
    }
}
