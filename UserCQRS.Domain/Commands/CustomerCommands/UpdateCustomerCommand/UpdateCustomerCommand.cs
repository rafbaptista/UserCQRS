using MediatR;

namespace UserCQRS.Domain.Commands.CustomerCommands.UpdateCustomerCommand
{
    public class UpdateCustomerCommand: Command, IRequest<CommandResult>
    {
        public UpdateCustomerCommand(int id, string firstName, string lastName, string email, string address, int age)
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

        public override bool IsValid()
        {
            ValidationResult = new UpdateCustomerCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
