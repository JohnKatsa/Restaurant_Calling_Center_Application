using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FinalProjectMVC.Domain
{
    public class Order
    {
        [Key]
        public int Id { get; set; }
        public DateTime PurchaseDate { get; set; }
        public float Cost { get; set; }
        public Guid ExposeId { get; set; }
        public ICollection<OrderedProduct> Products { get; set; }
    }
}