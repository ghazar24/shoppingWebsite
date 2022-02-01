﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace shoppingWebsite.Models
{
    public class AdviewModel
    {
        public int pro_id { get; set; }
        public string pro_name { get; set; }
        public string pro_image { get; set; }
        public string pro_description { get; set; }
        public Nullable<int> pro_price { get; set; }
        public int pro_fk_user { get; set; }
        public int pro_fk_cat { get; set; }
        public int cat_id { get; set; }
        
        public string cat_name { get; set; }
        public string u_name { get; set; }
        public string u_image { get; set; }
        public string u_contact { get; set; }


    }
}