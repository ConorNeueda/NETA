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
    
    public partial class la_pop_speed_ranked
    {
        public int id { get; set; }
        public string authority { get; set; }
        public long pop_size { get; set; }
        public Nullable<decimal> average_sync_speed { get; set; }
        public int pop_ranking { get; set; }
        public int sync_ranking { get; set; }
    }
}
