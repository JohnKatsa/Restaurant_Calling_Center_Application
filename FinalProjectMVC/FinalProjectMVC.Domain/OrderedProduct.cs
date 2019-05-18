using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FinalProjectMVC.Domain
{
    public class OrderedProduct
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public Category Category { get; set; }
        public string Description { get; set; }
        public float Cost { get; set; }
        public int Quantity { get; set; }
    }
}
