using InvoicesControl.Application.Interfaces.Repositories;
using InvoicesControl.Application.Services;
using InvoicesControl.Application.ViewModels;
using InvoicesControl.Domain.Entities;
using Moq.AutoMock;
using System;
using System.Threading.Tasks;
using Xunit;

namespace InvoicesControl.Tests.Services
{
    public class CustomerServiceTests
    {
        [Fact(DisplayName = "Add a valid customer")]
        [Trait("Category", "Customer Service AutoMock Tests")]
        public async Task CustomerService_Add_Must_Execute_Successfully()
        {
            // Arrange
            var mocker = new AutoMocker();
            var customerService = mocker.CreateInstance<CustomerService>();
            var customerVM = GenerateCustomerVM();

            // Act
            var customerId = await customerService.Add(customerVM);

            // Assert 
            Assert.True(customerId.HasValue && customerId != Guid.Empty);
        }

        [Fact(DisplayName = "Add a invalid customer")]
        [Trait("Category", "Customer Service AutoMock Tests")]
        public async Task CustomerService_Add_Must_Execute_With_Error()
        {
            // Arrange
            var mocker = new AutoMocker();
            var customerService = mocker.CreateInstance<CustomerService>();
            var customerVM = new CustomerVm();

            // Act
            var customerId = await customerService.Add(customerVM);

            // Assert 
            Assert.True(!customerId.HasValue);
        }

        [Fact(DisplayName = "Update a valid customer")]
        [Trait("Category", "Customer Service AutoMock Tests")]
        public async Task CustomerService_Update_Must_Execute_Successfully()
        {
            // Arrange
            var mocker = new AutoMocker();
            var customerService = mocker.CreateInstance<CustomerService>();
            var customerVM = GenerateCustomerVM();
            var customerId = await customerService.Add(customerVM);
            var customerEditVM = GenerateCustomerForEditing(customerId.Value);
            mocker.GetMock<ICustomerRepository>().Setup(c => c.GetById(customerId.Value))
                .Returns(GenerateCustomer());

            // Act
            var succeeded = await customerService.Update(customerEditVM);

            // Assert 
            Assert.True(succeeded);
        }

        [Fact(DisplayName = "Update a invalid customer")]
        [Trait("Category", "Customer Service AutoMock Tests")]
        public async Task CustomerService_Update_Must_Execute_With_Error()
        {
            // Arrange
            var mocker = new AutoMocker();
            var customerService = mocker.CreateInstance<CustomerService>();
            var customerEditVM = GenerateCustomerForEditing(Guid.NewGuid());

            // Act
            var succeeded = await customerService.Update(customerEditVM);

            Assert.False(succeeded);
        }

        private Task<Customer> GenerateCustomer()
        {
            return Task.FromResult(new Customer("23562", "Company A", "Company A"));
        }

        private CustomerVm GenerateCustomerVM()
        {
            return new CustomerVm
            {
                Document = "23562",
                CommercialName = "Company A",
                LegalName = "Company A"
            };
        }

        private CustomerEditVm GenerateCustomerForEditing(Guid id)
        {
            return new CustomerEditVm
            {
                Id = id,
                Document = "85685",
                CommercialName = "Company B",
                LegalName = "Company B"
            };
        }
    }
}
