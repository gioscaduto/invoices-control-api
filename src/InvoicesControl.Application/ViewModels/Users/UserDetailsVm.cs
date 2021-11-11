using InvoicesControl.Domain.Entities;
using System;

namespace InvoicesControl.Application.ViewModels.Users
{
    public class UserDetailsVm
    {
        public Guid Id { get; set; }        
        public string Name { get; set; }        
        public string Email { get; set; }        
        public string Document { get; set; }
        public string CompanyName { get; set; }
        public string PhoneNumber { get; set; }
        
        public UserDetailsVm(Person person)
        {
            Id = Guid.Parse(person.UserId);
            Name = person.Name;
            Email = person.Email;
            Document = person.Document;
            CompanyName = person.CompanyName;
            PhoneNumber = person.PhoneNumber;
        }
    }
}
