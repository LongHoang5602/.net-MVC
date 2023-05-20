using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Web.Models
{
    public class Products
    {
        public Category category { get; set; }
        public int IdCategory { get; set; }
        [Key]
        public int ProductId{ get; set; }
        [StringLength(255)]
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }= string.Empty;
        public Decimal ProductPrice { get; set; }

        public string ImageProduct { get; set; }

        

    

    }
}