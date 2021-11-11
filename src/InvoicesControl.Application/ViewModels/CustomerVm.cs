using InvoicesControl.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace InvoicesControl.Application.ViewModels
{
    public class CustomerListVm
    {
        public CustomerListVm(IEnumerable<Customer> customers)
        {
            Customers = customers?.Select(c => new CustomerDetailVm(c));
            Count = customers?.Count() ?? 0;
        }

        public int Count { get; private set; }
        public IEnumerable<CustomerDetailVm> Customers { get; private set; }
    }

    public class CustomerVm
    {
        [Required(ErrorMessage = "The field {0} is mandatory")]
        [StringLength(100, ErrorMessage = "The field {0} must has between {2} and {1} characters", MinimumLength = 4)]
        public string Document { get; set; }

        [Required(ErrorMessage = "The field {0} is mandatory")]
        [StringLength(255, ErrorMessage = "The field {0} must has between {2} and {1} characters", MinimumLength = 2)]
        public string CommercialName { get; set; }

        [Required(ErrorMessage = "The field {0} is mandatory")]
        [StringLength(255, ErrorMessage = "The field {0} must has between {2} and {1} characters", MinimumLength = 2)]
        public string LegalName { get; set; }

        public Customer ToCustomer()
        {
            return new Customer(Document, CommercialName, LegalName);
        }
    }

    public class CustomerEditVm
    {
        [Key]
        [Required(ErrorMessage = "The field {0} is mandatory")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "The field {0} is mandatory")]
        [StringLength(100, ErrorMessage = "The field {0} must has between {2} and {1} characters", MinimumLength = 4)]
        public string Document { get; set; }

        [Required(ErrorMessage = "The field {0} is mandatory")]
        [StringLength(255, ErrorMessage = "The field {0} must has between {2} and {1} characters", MinimumLength = 2)]
        public string CommercialName { get; set; }

        [Required(ErrorMessage = "The field {0} is mandatory")]
        [StringLength(255, ErrorMessage = "The field {0} must has between {2} and {1} characters", MinimumLength = 2)]
        public string LegalName { get; set; }

        public void UpdateCustomer(Customer customer)
        {
            customer.Update(Document, CommercialName, LegalName);
        }
    }

    public class CustomerDetailVm
    {
        public CustomerDetailVm(Customer customer)
        {
            Id = customer.Id;
            Document = customer.Document;
            CommercialName = customer.CommercialName;
            LegalName = customer.LegalName;
            Active = customer.Active;
        }

        public Guid Id { get; set; }
        public string Document { get; set; }
        public string CommercialName { get; set; }
        public string LegalName { get; set; }
        public bool Active { get; set; }
    }
}
