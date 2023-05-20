using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Web;

namespace Web.Models
{
    public class Item
    {
        public Products products { get; set; }
        public int Quantity { get; set; }
    }
}