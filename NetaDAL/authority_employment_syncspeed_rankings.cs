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
    
    public partial class authority_employment_syncspeed_rankings
    {
        public int authority_id { get; set; }
        public string authority_name { get; set; }
        public decimal sync_speed { get; set; }
        public int employment_rate { get; set; }
        public Nullable<int> speed_ranking { get; set; }
        public Nullable<int> employment_ranking { get; set; }
    }
}