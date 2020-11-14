using FluentValidation;

namespace UserCQRS.Domain.Commands.CustomerCommands.AddCustomerCommand
{
    public class AddCustomerCommandValidation : AbstractValidator<AddCustomerCommand>
    {
        public AddCustomerCommandValidation()
        {
            RuleFor(c => c.FirstName).NotEmpty().MinimumLength(3).WithMessage("Nome inválido");
            RuleFor(c => c.LastName).NotEmpty().MinimumLength(3).WithMessage("Sobrenome inválido");
            RuleFor(c => c.Email).NotNull().EmailAddress().WithMessage("E-mail Inválido");
            RuleFor(c => c.Address).NotEmpty().MinimumLength(3).WithMessage("Endereço inválido");
            RuleFor(c => c.Age).NotEmpty().GreaterThan(0).WithMessage("Idade obrigatória");
        }
    }
}
