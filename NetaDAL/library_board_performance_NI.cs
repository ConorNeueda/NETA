//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace NetaDAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class library_board_performance_NI
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public library_board_performance_NI()
        {
            this.local_authority_data = new HashSet<local_authority_data>();
        }
    
        public int id { get; set; }
        public string library_board { get; set; }
        public decimal percent_7_pass_grades { get; set; }
        public decimal percent_5_pass_grades { get; set; }
        public decimal percent_7_pass_grades_non_grammar { get; set; }
        public decimal percent_5_pass_grades_non_grammar { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<local_authority_data> local_authority_data { get; set; }
    }
}
