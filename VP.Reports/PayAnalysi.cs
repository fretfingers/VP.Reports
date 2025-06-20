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
    
    public partial class PayAnalysi
    {
        public string Company { get; set; }
        public string Branch { get; set; }
        public string EmployeeID { get; set; }
        public string PayTypeID { get; set; }
        public string AttrID { get; set; }
        public System.DateTime Tran_Date { get; set; }
        public Nullable<double> Amount { get; set; }
        public Nullable<double> PayTypeBalance { get; set; }
        public string LockedBy { get; set; }
        public Nullable<System.DateTime> LockTS { get; set; }
        public string PayTypeDescription { get; set; }
        public Nullable<bool> Taxable { get; set; }
        public Nullable<bool> ActiveYN { get; set; }
        public Nullable<double> ExactAmount { get; set; }
        public Nullable<bool> OnPayroll { get; set; }
        public Nullable<double> Rate { get; set; }
        public Nullable<double> Units { get; set; }
        public string GLAccount { get; set; }
        public string GLAccountEmployer { get; set; }
        public string GLAccountEmployerExp { get; set; }
        public string CurrencyID { get; set; }
        public Nullable<double> CurrencyExchangeRate { get; set; }
        public Nullable<bool> Prorate { get; set; }
    }
}
