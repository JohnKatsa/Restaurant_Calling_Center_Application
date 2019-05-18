using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FinalProjectMVC.Domain
{
    public class Customer
    {
        [Key]
        [JsonProperty("BusinessEntityId")]
        public int Id { get; set; }

        [Display(Name = "First Name")]
        [Required(ErrorMessage = "Please fill this field")]
        public string FirstName { get; set; }

        [Display(Name = "Middle Name")]
        public string MiddleName { get; set; }

        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "Please fill this field")]
        public string LastName { get; set; }

        [Display(Name = "Phone Number")]
        [Required(ErrorMessage = "Please fill this field")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Address")]
        [Required(ErrorMessage = "Please fill this field")]
        public string Address { get; set; }

        [Display(Name = "City")]
        [Required(ErrorMessage = "Please fill this field")]
        public string City { get; set; }

        [Display(Name = "Postal Code")]
        [Required(ErrorMessage = "Please fill this field")]
        public string PostalCode { get; set; }

        public Guid ExposeId { get; set; }

        public ICollection<Order> Orders { get; set; }
    }
}