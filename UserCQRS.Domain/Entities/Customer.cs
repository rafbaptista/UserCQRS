namespace UserCQRS.Domain.Entities
{
    public class Customer
    {
        public Customer()
        {

        }

        public Customer(int id, string firstName, string lastName, string email, string address, int age)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Address = address;
            Age = age;
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public int Age { get; set; }
    }
}
