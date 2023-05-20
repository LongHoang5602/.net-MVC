using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Web.Models
{
    public class Category
    {
        [Key]
        public int IdCategory { get; set; }
        public string Name { get; set; }
    }
}