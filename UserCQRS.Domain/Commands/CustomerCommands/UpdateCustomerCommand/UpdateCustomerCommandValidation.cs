using FluentValidation;

namespace UserCQRS.Domain.Commands.CustomerCommands.UpdateCustomerCommand
{
    public class UpdateCustomerCommandValidation : AbstractValidator<UpdateCustomerCommand>
    {
        public UpdateCustomerCommandValidation()
        {
            RuleFor(c => c.Id).GreaterThan(0).NotEmpty();
            RuleFor(c => c.FirstName).NotEmpty();
            RuleFor(c => c.LastName).NotEmpty();
            RuleFor(c => c.Email).EmailAddress();
            RuleFor(c => c.Address).NotEmpty();
            RuleFor(c => c.Age).NotEmpty();
        }
    }
}
