//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebApi.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class variation
    {
        public int ProcedureID { get; set; }
        public int VariationID { get; set; }
        public string Header { get; set; }
        public string SubHeader { get; set; }
    
        public virtual procedure procedure { get; set; }
    }
}
