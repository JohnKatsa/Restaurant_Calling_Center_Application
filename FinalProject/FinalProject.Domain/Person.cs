using System;
using System.Collections.Generic;

namespace FinalProject.Domain
{
    public partial class Person
    {
        public Person()
        {
            EmailAddress = new HashSet<EmailAddress>();
            PersonPhone = new HashSet<PersonPhone>();
        }

        public int BusinessEntityId { get; set; }
        public string PersonType { get; set; }
        public bool NameStyle { get; set; }
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Suffix { get; set; }
        public int EmailPromotion { get; set; }
        public string AdditionalContactInfo { get; set; }
        public string Demographics { get; set; }
        public Guid Rowguid { get; set; }
        public DateTime ModifiedDate { get; set; }

        public virtual Employee Employee { get; set; }
        public virtual ICollection<EmailAddress> EmailAddress { get; set; }
        public virtual ICollection<PersonPhone> PersonPhone { get; set; }
    }
}
