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
    using System.ComponentModel.DataAnnotations;
    
    public partial class table_category
    {
        public table_category()
        {
            this.table_product = new HashSet<table_product>();
        }
    
        public int cat_id { get; set; }
        [Required(ErrorMessage ="*")]
        public string cat_name { get; set; }
        [Required(ErrorMessage = "*")]
        public string cat_image { get; set; }
        public int cat_fk_ad { get; set; }
        public string cat_statu { get; set; }

        public virtual table_admin table_admin { get; set; }
        public virtual ICollection<table_product> table_product { get; set; }
    }
}