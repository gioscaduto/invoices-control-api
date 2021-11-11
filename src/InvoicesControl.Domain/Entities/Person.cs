namespace InvoicesControl.Domain.Entities
{
    public class Person : Entity
    {
        public Person(string name, string document, string companyName, string phoneNumber, string email, string userId)
        {
            Name = name;
            Document = document;
            CompanyName = companyName;
            PhoneNumber = phoneNumber;
            Email = email;
            UserId = userId;
        }

        protected Person() { }

        public string Name { get; private set; }
        public string Document { get; private set; }
        public string CompanyName { get; private set; }
        public string PhoneNumber { get; private set; }
        public string Email { get; private set; }
        public string UserId { get; private set; }

        public void Update(string name, string document, string companyName, string phoneNumber, string email)
        {
            Name = name;
            Document = document;
            CompanyName = companyName;
            PhoneNumber = phoneNumber;
            Email = email;
        }
    }
}
