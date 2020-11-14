using MediatR;

namespace UserCQRS.Domain.Commands.CustomerCommands.AddCustomerCommand
{
    public class AddCustomerCommand : Command, IRequest<CommandResult>
    {
        public AddCustomerCommand(string firstName, string lastName, string email, string address, int age)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Address = address;
            Age = age;
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public int Age { get; set; }       

        public override bool IsValid()
        {
            ValidationResult = new AddCustomerCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
