using System.ComponentModel.DataAnnotations;

namespace FinalProjectMVC.Domain
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public Category Category { get; set; }
        public string Description { get; set; }
        public float Cost { get; set; }
    }
}