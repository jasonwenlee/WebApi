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
    
    public partial class complication
    {
        public int ProcedureID { get; set; }
        public int ComplicationID { get; set; }
        public string Name { get; set; }
        public string DiagramURL { get; set; }
        public Nullable<int> Number { get; set; }
    
        public virtual procedure procedure { get; set; }
    }
}
