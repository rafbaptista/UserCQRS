using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace UserCQRS.Domain.Commands.CustomerCommands.DeleteCustomerCommand
{
    public class DeleteCustomerCommandValidation : AbstractValidator<DeleteCustomerCommand>
    {
        public DeleteCustomerCommandValidation()
        {
            RuleFor(c => c.Id).NotNull().GreaterThan(0);
        }
    }
}
