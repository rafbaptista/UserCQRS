using MediatR;
using UserCQRS.Domain.Interfaces.Commands;

namespace UserCQRS.Domain.Commands.CustomerCommands.DeleteCustomerCommand
{
    public class DeleteCustomerCommand : Command, IRequest<ICommandResult>
    {
        public DeleteCustomerCommand(int id)
        {
            Id = id;
        }

        public int Id { get; set; }

        public override bool IsValid()
        {
            ValidationResult = new DeleteCustomerCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
