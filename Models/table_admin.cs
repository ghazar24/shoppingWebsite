//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace shoppingWebsite.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class table_admin
    {
        public table_admin()
        {
            this.table_category = new HashSet<table_category>();
        }
    
        public int ad_id { get; set; }
        public string ad_userName { get; set; }
        public string ad_password { get; set; }
    
        public virtual ICollection<table_category> table_category { get; set; }
    }
}
